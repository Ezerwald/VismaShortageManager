using System.Text.Json;
using VismaShortageManager.src.Data;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Models;
using System.Text.Json.Serialization;

namespace VismaShortageManager.Tests.Data
{
    public class ShortageRepositoryTests
    {
        private readonly string _filePath = "shortages_test.json";

        [Fact]
        public void GetAllShortages_ShouldReturnListOfShortages_WhenFileContainsValidData()
        {
            // Arrange
            var shortages = new List<Shortage>
            {
                new Shortage { Title = "Test Shortage 1", Room = RoomType.Kitchen, Priority = 1 },
                new Shortage { Title = "Test Shortage 2", Room = RoomType.Bathroom, Priority = 2 }
            };
            var jsonData = JsonSerializer.Serialize(shortages, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            });
            File.WriteAllText(_filePath, jsonData);

            var repository = new ShortageRepository(_filePath);

            // Act
            var result = repository.GetAllShortages();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Test Shortage 1", result[0].Title);
            Assert.Equal("Test Shortage 2", result[1].Title);

            // Cleanup
            File.Delete(_filePath);
        }

        [Fact]
        public void GetAllShortages_ShouldReturnEmptyList_WhenFileIsEmpty()
        {
            // Arrange
            File.WriteAllText(_filePath, string.Empty);
            var repository = new ShortageRepository(_filePath);

            // Act
            var result = repository.GetAllShortages();

            // Assert
            Assert.Empty(result);

            // Cleanup
            File.Delete(_filePath);
        }

        [Fact]
        public void GetAllShortages_ShouldReturnEmptyList_WhenFileDoesNotExist()
        {
            // Arrange
            var repository = new ShortageRepository(_filePath);

            // Act
            var result = repository.GetAllShortages();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SaveShortages_ShouldWriteShortagesToFile()
        {
            // Arrange
            var shortages = new List<Shortage>
            {
                new Shortage { Title = "Test Shortage 1", Room = RoomType.Kitchen, Priority = 1 },
                new Shortage { Title = "Test Shortage 2", Room = RoomType.Bathroom, Priority = 2 }
            };
            var repository = new ShortageRepository(_filePath);

            // Act
            repository.SaveShortages(shortages);

            // Assert
            var jsonData = File.ReadAllText(_filePath);
            var result = JsonSerializer.Deserialize<List<Shortage>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Test Shortage 1", result[0].Title);
            Assert.Equal("Test Shortage 2", result[1].Title);

            // Cleanup
            File.Delete(_filePath);
        }
    }
}
