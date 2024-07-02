using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using System;
using System.IO;
using Xunit;

namespace VismaShortageManager.Tests.Helpers
{
    public class InputParserTests : IDisposable
    {
        private readonly InputParser _inputParser;
        private StringWriter _consoleOutput;
        private StringReader _consoleInput;

        public InputParserTests()
        {
            _inputParser = new InputParser();
            _consoleOutput = new StringWriter();
            _consoleInput = new StringReader(string.Empty);
            Console.SetOut(_consoleOutput);
            Console.SetIn(_consoleInput);
        }

        public void Dispose()
        {
            _consoleOutput.Dispose();
            _consoleInput.Dispose();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            });
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void ParseEnum_ShouldReturnCorrectEnum()
        {
            // Arrange
            var input = "1\n";
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

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
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

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
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

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
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

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
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

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
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

            // Act
            var result = _inputParser.ParseBool("Is it true?");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ParseDateTime_ShouldReturnDateTime()
        {
            // Arrange
            var input = "2005-11-16\n";
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

            // Act
            var result = _inputParser.ParseDateTime("Enter a date:");

            // Assert
            Assert.Equal(new DateTime(2005, 11, 16), result);
        }

        [Fact]
        public void ParseDateTime_ShouldReturnNull()
        {
            // Arrange
            var input = "\n";
            _consoleInput = new StringReader(input);
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

            // Act
            var result = _inputParser.ParseDateTime("Enter a date:");

            // Assert
            Assert.Null(result);
        }
    }
}
