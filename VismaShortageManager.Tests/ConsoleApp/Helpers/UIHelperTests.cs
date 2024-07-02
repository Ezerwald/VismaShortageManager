using VismaShortageManager.src.ConsoleApp.Helpers;

namespace VismaShortageManager.Tests.ConsoleApp.Helpers
{
    public class UIHelperTests : IDisposable
    {
        private StringWriter _consoleOutput;
        private StringReader _consoleInput;

        public UIHelperTests()
        {
            ResetConsole();
        }

        public void Dispose()
        {
            ResetConsole();
        }

        private void ResetConsole()
        {
            _consoleOutput?.Dispose();
            _consoleInput?.Dispose();

            _consoleOutput = new StringWriter();
            _consoleInput = new StringReader(string.Empty);

            Console.SetOut(_consoleOutput);
            Console.SetIn(_consoleInput);
        }

        [Fact]
        public void SeparateMessage_ShouldDisplaySeparatorLine()
        {
            // Arrange
            ResetConsole();

            // Act
            UIHelper.SeparateMessage('*', 10);

            // Assert
            var expectedOutput = "**********\r\n";
            Assert.Equal(expectedOutput, _consoleOutput.ToString());
        }

        [Fact]
        public void ShowInvalidInputResponse_ShouldDisplayWarningMessageAndWaitForKeyPress()
        {
            // Arrange
            var expectedOutput = "Invalid option, please try again";

            // Set input and output
            _consoleInput = new StringReader("a\n");
            _consoleOutput = new StringWriter();
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

            // Act
            UIHelper.ShowInvalidInputResponse();

            // Assert
            var actualOutput = RemoveConsoleColorCodes(_consoleOutput.ToString());
            Assert.Contains(expectedOutput, actualOutput);
        }


        [Fact]
        public void ShowSuccessMessage_ShouldDisplaySuccessMessageInGreen()
        {
            // Arrange
            ResetConsole();

            // Act
            UIHelper.ShowSuccessMessage("Success");

            // Assert
            var expectedOutput = "Success";
            Assert.Contains(expectedOutput, RemoveConsoleColorCodes(_consoleOutput.ToString()));
        }

        [Fact]
        public void ShowWarningMessage_ShouldDisplayWarningMessageInRed()
        {
            // Arrange
            ResetConsole();

            // Act
            UIHelper.ShowWarningMessage("Warning");

            // Assert
            var expectedOutput = "Warning";
            Assert.Contains(expectedOutput, RemoveConsoleColorCodes(_consoleOutput.ToString()));
        }

        [Fact]
        public void ShowInfoMessage_ShouldDisplayInfoMessageInYellow()
        {
            // Arrange
            ResetConsole();

            // Act
            UIHelper.ShowInfoMessage("Info");

            // Assert
            var expectedOutput = "Info";
            Assert.Contains(expectedOutput, RemoveConsoleColorCodes(_consoleOutput.ToString()));
        }

        private string RemoveConsoleColorCodes(string input)
        {
            // Remove ANSI color codes (console color formatting) and trim extra whitespace
            return System.Text.RegularExpressions.Regex.Replace(input, @"\u001b\[[0-9;]*m", string.Empty).Trim();
        }
    }
}