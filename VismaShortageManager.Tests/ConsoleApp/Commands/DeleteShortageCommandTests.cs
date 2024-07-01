using Moq;
using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;

namespace VismaShortageManager.Tests.Commands
{
    public class DeleteShortageCommandTests
    {
        private readonly Mock<ShortageService> _shortageServiceMock;
        private readonly Mock<IInputParser> _inputParserMock;
        private readonly DeleteShortageCommand _deleteShortageCommand;
        private readonly User _testUser;

        public DeleteShortageCommandTests()
        {
            _shortageServiceMock = new Mock<ShortageService>();
            _inputParserMock = new Mock<IInputParser>();
            _deleteShortageCommand = new DeleteShortageCommand(_shortageServiceMock.Object, _inputParserMock.Object);
            _testUser = new User { Name = "Test User", IsAdministrator = true };
            _deleteShortageCommand.SetUser(_testUser);
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
            _shortageServiceMock.Setup(service => service.DeleteShortage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()))
                .Throws(new Exception("Test Exception"));

            // Act and Assert
            var ex = Record.Exception(() => _deleteShortageCommand.Execute());
            Assert.Null(ex); // No exception should propagate out of the Execute method
        }
    }
}
