using VismaShortageManager.src.Data;

namespace VismaShortageManager.Tests.Data
{
    [Collection("DataTests")]
    public class JsonFileHandlerTests
    {
        [Fact]
        public void ReadJsonFromFile_ShouldReturnJsonData_WhenFileExists()
        {
            // Arrange
            var filePath = "test.json";
            var expectedJson = "{\"key\":\"value\"}";
            File.WriteAllText(filePath, expectedJson);

            // Act
            var jsonData = JsonFileHandler.ReadJsonFromFile(filePath);

            // Assert
            Assert.Equal(expectedJson, jsonData);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void ReadJsonFromFile_ShouldReturnEmptyString_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = "nonexistent.json";

            // Act
            var jsonData = JsonFileHandler.ReadJsonFromFile(filePath);

            // Assert
            Assert.Equal(string.Empty, jsonData);
        }

        [Fact]
        public void WriteJsonToFile_ShouldWriteJsonDataToFile()
        {
            // Arrange
            var filePath = "test.json";
            var jsonData = "{\"key\":\"value\"}";

            // Act
            JsonFileHandler.WriteJsonToFile(filePath, jsonData);

            // Assert
            var writtenData = File.ReadAllText(filePath);
            Assert.Equal(jsonData, writtenData);

            // Cleanup
            File.Delete(filePath);
        }
    }
}
