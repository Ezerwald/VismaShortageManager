using Moq;
using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;
using VismaShortageManager.src.Domain.Interfaces;
using Xunit;

namespace VismaShortageManager.Tests.Commands
{
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
            _inputParserMock.Setup(x => x.ParseAnyString(It.IsAny<string>())).Returns("Test Title");

            _listShortagesCommand.AddFilter();

            Assert.Equal("Test Title", _listShortagesCommand.FilterTitle);
        }

        [Fact]
        public void AddFilter_ShouldAddCategoryFilter()
        {
            _inputParserMock.Setup(x => x.ParseEnum<CategoryType>(It.IsAny<string>())).Returns(CategoryType.Food);

            _listShortagesCommand.AddFilter();

            Assert.Equal(CategoryType.Food, _listShortagesCommand.FilterCategory);
        }

        [Fact]
        public void AddFilter_ShouldAddRoomFilter()
        {
            _inputParserMock.Setup(x => x.ParseEnum<RoomType>(It.IsAny<string>())).Returns(RoomType.Kitchen);

            _listShortagesCommand.AddFilter();

            Assert.Equal(RoomType.Kitchen, _listShortagesCommand.FilterRoom);
        }

        [Fact]
        public void AddFilter_ShouldAddDateRangeFilter()
        {
            var startDate = new DateTime(2005, 11, 16);
            var endDate = new DateTime(2006, 11, 23);

            _inputParserMock.SetupSequence(x => x.ParseDateTime(It.IsAny<string>()))
                .Returns(startDate)
                .Returns(endDate);

            _listShortagesCommand.AddFilter();

            Assert.Equal(startDate, _listShortagesCommand.FilterDateStart);
            Assert.Equal(endDate, _listShortagesCommand.FilterDateEnd);
        }

        [Fact]
        public void ClearFilters_ShouldClearAllFilters()
        {
            _listShortagesCommand.AddFilter();
            _listShortagesCommand.ClearFilters();

            Assert.Null(_listShortagesCommand.FilterTitle);
            Assert.Null(_listShortagesCommand.FilterCategory);
            Assert.Null(_listShortagesCommand.FilterRoom);
            Assert.Null(_listShortagesCommand.FilterDateStart);
            Assert.Null(_listShortagesCommand.FilterDateEnd);
        }
    }
}
