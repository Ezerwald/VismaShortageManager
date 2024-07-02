using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;
using Xunit;

namespace VismaShortageManager.Tests.Commands
{
    [Collection("AddShortageCommandTests")]
    public class AddShortageCommandTests : IDisposable
    {
        private readonly Mock<IShortageRepository> _shortageRepositoryMock;
        private readonly Mock<IShortageService> _shortageServiceMock;
        private readonly Mock<IInputParser> _inputParserMock;
        private readonly AddShortageCommand _addShortageCommand;
        private readonly User _testUser;

        private StringWriter _consoleOutput;
        private StringReader _consoleInput;

        public AddShortageCommandTests()
        {
            _shortageRepositoryMock = new Mock<IShortageRepository>();
            _shortageServiceMock = new Mock<IShortageService>();
            _inputParserMock = new Mock<IInputParser>();

            var services = new ServiceCollection();
            services.AddSingleton(_shortageRepositoryMock.Object);
            services.AddSingleton(_shortageServiceMock.Object);
            services.AddSingleton(_inputParserMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            _addShortageCommand = new AddShortageCommand(
                serviceProvider.GetRequiredService<IShortageService>(),
                serviceProvider.GetRequiredService<IInputParser>()
            );

            _testUser = new User { Name = "Test User", IsAdministrator = true };
            _addShortageCommand.SetUser(_testUser);

            _consoleOutput = new StringWriter();
            _consoleInput = new StringReader(string.Empty);
            Console.SetOut(_consoleOutput);
            Console.SetIn(_consoleInput);
        }

        public void Dispose()
        {
            _consoleOutput.Dispose();
            _consoleInput.Dispose();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            });
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void Execute_ShouldAddShortage()
        {
            // Arrange
            var shortage = new Shortage
            {
                Title = "Test Shortage",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 5,
                CreatedBy = _testUser.Name,
                CreatedOn = DateTime.Now
            };

            _inputParserMock.Setup(x => x.ParseNonEmptyString(It.IsAny<string>())).Returns(shortage.Title);
            _inputParserMock.Setup(x => x.ParseEnum<RoomType>(It.IsAny<string>())).Returns(shortage.Room);
            _inputParserMock.Setup(x => x.ParseEnum<CategoryType>(It.IsAny<string>())).Returns(shortage.Category);
            _inputParserMock.Setup(x => x.ParseIntInRange(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(shortage.Priority);

            // Act
            _addShortageCommand.Execute();

            // Assert
            _shortageServiceMock.Verify(service => service.AddShortage(It.Is<Shortage>(s =>
                s.Title == shortage.Title &&
                s.Room == shortage.Room &&
                s.Category == shortage.Category &&
                s.Priority == shortage.Priority &&
                s.CreatedBy == shortage.CreatedBy
            )), Times.Once);
        }

        [Fact]
        public void Execute_ShouldHandleException()
        {
            // Arrange
            _shortageServiceMock.Setup(service => service.AddShortage(It.IsAny<Shortage>()))
                .Throws(new Exception("Test Exception"));

            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);

            // Act
            _addShortageCommand.Execute();

            // Assert
            var output = _consoleOutput.ToString();
            Assert.Contains("An error occurred: Test Exception", output);
        }
    }
}
