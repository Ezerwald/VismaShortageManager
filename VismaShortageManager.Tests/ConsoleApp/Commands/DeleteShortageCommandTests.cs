using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;
using Xunit;

namespace VismaShortageManager.Tests.Commands
{
    [Collection("DeleteShortageCommandTests")]
    public class DeleteShortageCommandTests : IDisposable
    {
        private readonly Mock<IShortageRepository> _shortageRepositoryMock;
        private readonly Mock<IShortageService> _shortageServiceMock;
        private readonly Mock<IInputParser> _inputParserMock;
        private readonly DeleteShortageCommand _deleteShortageCommand;
        private readonly User _testUser;

        private StringWriter _consoleOutput;
        private StringReader _consoleInput;

        public DeleteShortageCommandTests()
        {
            _shortageRepositoryMock = new Mock<IShortageRepository>();
            _shortageServiceMock = new Mock<IShortageService>();
            _inputParserMock = new Mock<IInputParser>();

            var services = new ServiceCollection();
            services.AddSingleton(_shortageRepositoryMock.Object);
            services.AddSingleton(_shortageServiceMock.Object);
            services.AddSingleton(_inputParserMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            _deleteShortageCommand = new DeleteShortageCommand(
                serviceProvider.GetRequiredService<IShortageService>(),
                serviceProvider.GetRequiredService<IInputParser>()
            );

            _testUser = new User { Name = "Test User", IsAdministrator = true };
            _deleteShortageCommand.SetUser(_testUser);

            // Redirect Console output and input
            _consoleOutput = new StringWriter();
            _consoleInput = new StringReader(string.Empty);
            Console.SetOut(_consoleOutput);
            Console.SetIn(_consoleInput);
        }

        public void Dispose()
        {
            // Clean up Console redirection
            _consoleOutput.Dispose();
            _consoleInput.Dispose();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            });
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void Execute_ShouldDeleteShortage()
        {
            // Arrange
            var title = "Test Shortage";
            var room = RoomType.Kitchen.ToString();

            _inputParserMock.Setup(x => x.ParseNonEmptyString(It.IsAny<string>())).Returns(title);
            _inputParserMock.Setup(x => x.ParseEnum<RoomType>(It.IsAny<string>())).Returns(RoomType.Kitchen);

            // Act
            _deleteShortageCommand.Execute();

            // Assert
            _shortageServiceMock.Verify(service => service.DeleteShortage(title, room, _testUser), Times.Once);
        }

        [Fact]
        public void Execute_ShouldHandleException()
        {
            // Arrange
            var expectedErrorMessage = "Test Exception";
            _shortageServiceMock.Setup(service => service.DeleteShortage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()))
                .Throws(new Exception(expectedErrorMessage));

            try
            {
                // Act
                _deleteShortageCommand.Execute();
            }
            catch (Exception)
            {
                // Swallow any exceptions thrown during execution
            }

            // Assert
            var output = _consoleOutput.ToString();
            Assert.Contains($"An error occurred: {expectedErrorMessage}", output);
        }

    }
}
