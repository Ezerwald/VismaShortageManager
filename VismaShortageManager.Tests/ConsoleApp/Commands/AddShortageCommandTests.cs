using System;
using Moq;
using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;
using Xunit;

namespace VismaShortageManager.Tests.Commands
{
    public class AddShortageCommandTests
    {
        private readonly Mock<ShortageService> _shortageServiceMock;
        private readonly Mock<IInputParser> _inputParserMock;
        private readonly AddShortageCommand _addShortageCommand;
        private readonly User _testUser;

        public AddShortageCommandTests()
        {
            _shortageServiceMock = new Mock<ShortageService>();
            _inputParserMock = new Mock<IInputParser>();
            _addShortageCommand = new AddShortageCommand(_shortageServiceMock.Object, _inputParserMock.Object);
            _testUser = new User { Name = "Test User", IsAdministrator = true };
            _addShortageCommand.SetUser(_testUser);
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

            // Act and Assert
            var ex = Record.Exception(() => _addShortageCommand.Execute());
            Assert.Null(ex); // No exception should propagate out of the Execute method
        }
    }
}
