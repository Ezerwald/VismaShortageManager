using Moq;
using System;
using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using Xunit;

namespace VismaShortageManager.Tests.Helpers
{
    public class InputParserTests
    {
        private readonly Mock<IInputParserMethods> _inputParserMethodsMock;
        private readonly InputParser _inputParser;

        public InputParserTests()
        {
            _inputParserMethodsMock = new Mock<IInputParserMethods>();
            _inputParser = new InputParser();
        }

        [Fact]
        public void ParseEnum_ShouldReturnCorrectEnum()
        {
            // Arrange
            var expectedEnum = RoomType.Kitchen;
            _inputParserMethodsMock.Setup(m => m.ParseEnum<RoomType>(It.IsAny<string>()))
                .Returns(expectedEnum);

            // Act
            var result = _inputParser.ParseEnum<RoomType>("Select a room:");

            // Assert
            Assert.Equal(expectedEnum, result);
        }

        [Fact]
        public void ParseIntInRange_ShouldReturnValidInteger()
        {
            // Arrange
            var expectedInt = 5;
            _inputParserMethodsMock.Setup(m => m.ParseIntInRange(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedInt);

            // Act
            var result = _inputParser.ParseIntInRange("Enter a number between 1 and 10:", 1, 10);

            // Assert
            Assert.Equal(expectedInt, result);
        }

        [Fact]
        public void ParseNonEmptyString_ShouldReturnValidString()
        {
            // Arrange
            var expectedString = "Test String";
            _inputParserMethodsMock.Setup(m => m.ParseNonEmptyString(It.IsAny<string>()))
                .Returns(expectedString);

            // Act
            var result = _inputParser.ParseNonEmptyString("Enter a non-empty string:");

            // Assert
            Assert.Equal(expectedString, result);
        }

        [Fact]
        public void ParseAnyString_ShouldReturnString()
        {
            // Arrange
            var expectedString = "Any String";
            _inputParserMethodsMock.Setup(m => m.ParseAnyString(It.IsAny<string>()))
                .Returns(expectedString);

            // Act
            var result = _inputParser.ParseAnyString("Enter any string:");

            // Assert
            Assert.Equal(expectedString, result);
        }

        [Fact]
        public void ParseBool_ShouldReturnBoolean()
        {
            // Arrange
            var expectedBool = true;
            _inputParserMethodsMock.Setup(m => m.ParseBool(It.IsAny<string>()))
                .Returns(expectedBool);

            // Act
            var result = _inputParser.ParseBool("Is it true?");

            // Assert
            Assert.Equal(expectedBool, result);
        }

        [Fact]
        public void ParseDateTime_ShouldReturnDateTime()
        {
            // Arrange
            var expectedDateTime = DateTime.Now;
            _inputParserMethodsMock.Setup(m => m.ParseDateTime(It.IsAny<string>()))
                .Returns(expectedDateTime);

            // Act
            var result = _inputParser.ParseDateTime("Enter a date:");

            // Assert
            Assert.Equal(expectedDateTime, result);
        }
    }
}
