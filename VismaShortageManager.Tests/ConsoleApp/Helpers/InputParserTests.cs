using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;

namespace VismaShortageManager.Tests.Helpers
{
    public class InputParserTests
    {
        private readonly InputParser _inputParser;

        public InputParserTests()
        {
            _inputParser = new InputParser();
        }

        [Fact]
        public void ParseEnum_ShouldReturnCorrectEnum()
        {
            // Arrange
            var input = "1\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseEnum<RoomType>("Select a room:");

            // Assert
            Assert.Equal(RoomType.Kitchen, result);
        }

        [Fact]
        public void ParseIntInRange_ShouldReturnValidInteger()
        {
            // Arrange
            var input = "5\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseIntInRange("Enter a number between 1 and 10:", 1, 10);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void ParseNonEmptyString_ShouldReturnValidString()
        {
            // Arrange
            var input = "Test String\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseNonEmptyString("Enter a non-empty string:");

            // Assert
            Assert.Equal("Test String", result);
        }

        [Fact]
        public void ParseAnyString_ShouldReturnString()
        {
            // Arrange
            var input = "Any String\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseAnyString("Enter any string:");

            // Assert
            Assert.Equal("Any String", result);
        }

        [Fact]
        public void ParseBool_ShouldReturnTrue()
        {
            // Arrange
            var input = "yes\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseBool("Is it true?");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ParseBool_ShouldReturnFalse()
        {
            // Arrange
            var input = "no\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseBool("Is it true?");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ParseDateTime_ShouldReturnDateTime()
        {
            // Arrange
            var input = "2023-01-01\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseDateTime("Enter a date:");

            // Assert
            Assert.Equal(new DateTime(2023, 1, 1), result);
        }

        [Fact]
        public void ParseDateTime_ShouldReturnNull()
        {
            // Arrange
            var input = "\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);

            // Act
            var result = _inputParser.ParseDateTime("Enter a date:");

            // Assert
            Assert.Null(result);
        }
    }
}
