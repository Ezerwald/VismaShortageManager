using Moq;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;
using VismaShortageManager.src.Domain.Enums;

namespace VismaShortageManager.Tests.Services
{
    public class ShortageServiceTests
    {
        private readonly Mock<IShortageRepository> _repositoryMock;
        private readonly ShortageService _shortageService;
        private readonly User _testUser;
        private readonly List<Shortage> _shortages;

        public ShortageServiceTests()
        {
            _repositoryMock = new Mock<IShortageRepository>();
            _shortageService = new ShortageService(_repositoryMock.Object);
            _testUser = new User { Name = "Test User", IsAdministrator = true };

            _shortages = new List<Shortage>
            {
                new Shortage { Title = "Test Shortage 1", Room = RoomType.Kitchen, Category = CategoryType.Food, Priority = 1, CreatedBy = "Test User", CreatedOn = DateTime.Now },
                new Shortage { Title = "Test Shortage 2", Room = RoomType.MeetingRoom, Category = CategoryType.Electronics, Priority = 3, CreatedBy = "Another User", CreatedOn = DateTime.Now }
            };

            _repositoryMock.Setup(repo => repo.GetAllShortages()).Returns(_shortages);
        }

        // Add shortage method tests

        [Fact]
        public void AddShortage_ShouldAddNewShortage()
        {
            // Arrange
            var newShortage = new Shortage
            {
                Title = "New Shortage",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 2,
                CreatedBy = "Test User",
                CreatedOn = DateTime.Now
            };

            // Act
            _shortageService.AddShortage(newShortage);

            // Assert
            _repositoryMock.Verify(repo => repo.SaveShortages(It.Is<List<Shortage>>(s => s.Contains(newShortage))), Times.Once);
        }

        [Fact]
        public void AddShortage_ShouldUpdateExistingShortage_WhenPriorityIsHigher()
        {
            // Arrange
            var updatedShortage = new Shortage
            {
                Title = "Test Shortage 1",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 5,
                CreatedBy = "Test User",
                CreatedOn = DateTime.Now
            };

            // Act
            _shortageService.AddShortage(updatedShortage);

            // Assert
            _repositoryMock.Verify(repo => repo.SaveShortages(It.Is<List<Shortage>>(s => s.Any(x => x.Title == "Test Shortage 1" && x.Priority == 5))), Times.Once);
        }

        [Fact]
        public void AddShortage_ShouldNotUpdateExistingShortage_WhenPriorityIsLowerOrEqual()
        {
            // Arrange
            var updatedShortage = new Shortage
            {
                Title = "Test Shortage 1",
                Room = RoomType.Kitchen,
                Category = CategoryType.Food,
                Priority = 1,
                CreatedBy = "Test User",
                CreatedOn = DateTime.Now
            };

            // Act
            _shortageService.AddShortage(updatedShortage);

            // Assert
            _repositoryMock.Verify(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()), Times.Never);
        }

        [Fact]
        public void DeleteShortage_ShouldDeleteExistingShortage_WhenUserIsCreatorOrAdmin()
        {
            // Act
            _shortageService.DeleteShortage("Test Shortage 1", "Kitchen", _testUser);

            // Assert
            _repositoryMock.Verify(repo => repo.SaveShortages(It.Is<List<Shortage>>(s => !s.Any(x => x.Title == "Test Shortage 1" && x.Room == RoomType.Kitchen))), Times.Once);
        }

        // Delete shortage method tests

        [Fact]
        public void DeleteShortage_ShouldNotDeleteShortage_WhenUserIsNotAuthorized()
        {
            // Arrange
            var unauthorizedUser = new User { Name = "Unauthorized User", IsAdministrator = false };

            // Act
            _shortageService.DeleteShortage("Test Shortage 2", "Office", unauthorizedUser);

            // Assert
            _repositoryMock.Verify(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()), Times.Never);
        }

        [Fact]
        public void DeleteShortage_ShouldHandleNonExistentShortage()
        {
            // Act
            _shortageService.DeleteShortage("Nonexistent Shortage", "Kitchen", _testUser);

            // Assert
            _repositoryMock.Verify(repo => repo.SaveShortages(It.IsAny<List<Shortage>>()), Times.Never);
        }

        // List shortages method tests

        [Fact]
        public void ListShortages_ShouldReturnAllShortages_ForAdmin()
        {
            // Act
            var result = _shortageService.ListShortages(_testUser);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ListShortages_ShouldReturnUserShortages_ForNonAdmin()
        {
            // Arrange
            var nonAdminUser = new User { Name = "Test User", IsAdministrator = false };

            // Act
            var result = _shortageService.ListShortages(nonAdminUser);

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Shortage 1", result[0].Title);
        }

        [Fact]
        public void ListShortages_ShouldApplyFilters()
        {
            // Act
            var result = _shortageService.ListShortages(_testUser, filterTitle: "Test Shortage 2");

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Shortage 2", result[0].Title);
        }
    }
}
