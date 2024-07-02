using Moq;
using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;
using VismaShortageManager.src.Domain.Interfaces;
using Xunit;

namespace VismaShortageManager.Tests.Commands
{
    [Collection("ListShortagesCommandTests")]
    public class ListShortagesCommandTests
    {
        private readonly Mock<IShortageRepository> _shortageRepositoryMock;
        private readonly Mock<ShortageService> _shortageServiceMock;
        private readonly Mock<DeleteShortageCommand> _deleteShortageCommandMock;
        private readonly Mock<IInputParser> _inputParserMock;
        private readonly ListShortagesCommand _listShortagesCommand;
        private readonly User _testUser;

        public ListShortagesCommandTests()
        {
            _shortageRepositoryMock = new Mock<IShortageRepository>();
            _shortageServiceMock = new Mock<ShortageService>(_shortageRepositoryMock.Object);
            _inputParserMock = new Mock<IInputParser>();
            _deleteShortageCommandMock = new Mock<DeleteShortageCommand>(_shortageServiceMock.Object, _inputParserMock.Object);
            _listShortagesCommand = new ListShortagesCommand(_shortageServiceMock.Object, _deleteShortageCommandMock.Object, _inputParserMock.Object);
            _testUser = new User { Name = "Test User", IsAdministrator = true };
            _listShortagesCommand.SetUser(_testUser);
        }

        [Fact]
        public void AddFilter_ShouldAddTitleFilter()
        {
            // Arrange
            var input = "Test Title";

            _inputParserMock.Setup(x => x.ParseAnyString(It.IsAny<string>())).Returns(input);

            // Act
            _listShortagesCommand.AddFilterMenuOnSelect(1);

            // Assert
            Assert.Equal(input, _listShortagesCommand.FilterTitle);
        }

        [Fact]
        public void AddFilter_ShouldAddDateRangeFilter()
        {
            // Arrange
            var startDate = new DateTime(2005, 11, 16);
            var endDate = new DateTime(2006, 11, 23);

            _inputParserMock.SetupSequence(x => x.ParseDateTime(It.IsAny<string>()))
                .Returns(startDate)
                .Returns(endDate);

            // Act
            _listShortagesCommand.AddFilterMenuOnSelect(2);

            // Assert
            Assert.Equal(startDate, _listShortagesCommand.FilterDateStart);
            Assert.Equal(endDate, _listShortagesCommand.FilterDateEnd);
        }

        [Fact]
        public void AddFilter_ShouldAddCategoryFilter()
        {
            // Arrange
            var input = CategoryType.Food;

            _inputParserMock.Setup(x => x.ParseEnum<CategoryType>(It.IsAny<string>())).Returns(input);

            // Act
            _listShortagesCommand.AddFilterMenuOnSelect(3);

            // Assert
            Assert.Equal(input, _listShortagesCommand.FilterCategory);
        }

        [Fact]
        public void AddFilter_ShouldAddRoomFilter()
        {
            // Arrange
            var input = RoomType.Kitchen;

            _inputParserMock.Setup(x => x.ParseEnum<RoomType>(It.IsAny<string>())).Returns(input);

            // Act
            _listShortagesCommand.AddFilterMenuOnSelect(4);

            // Assert
            Assert.Equal(input, _listShortagesCommand.FilterRoom);
        }

        [Fact]
        public void ClearFilters_ShouldClearAllFilters()
        {   
            // Arrange
            _listShortagesCommand.ClearFilters();

            Assert.Null(_listShortagesCommand.FilterTitle);
            Assert.Null(_listShortagesCommand.FilterCategory);
            Assert.Null(_listShortagesCommand.FilterRoom);
            Assert.Null(_listShortagesCommand.FilterDateStart);
            Assert.Null(_listShortagesCommand.FilterDateEnd);
        }
    }
}
