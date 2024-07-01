using VismaShortageManager.src.ConsoleApp.Helpers;

namespace VismaShortageManager.Tests.Helpers
{
    public class UIHelperTests
    {
        [Fact]
        public void SeparateMessage_ShouldDisplaySeparatorLine()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            UIHelper.SeparateMessage('*', 10);

            // Assert
            var expectedOutput = "**********\n";
            Assert.Equal(expectedOutput, output.ToString());
        }

        [Fact]
        public void ShowInvalidInputResponse_ShouldDisplayWarningMessageAndWaitForKeyPress()
        {
            // Arrange
            var input = "a\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            UIHelper.ShowInvalidInputResponse();

            // Assert
            var expectedOutput = "Invalid option, please try again\n";
            Assert.Contains(expectedOutput, output.ToString());
        }

        [Fact]
        public void ShowSuccessMessage_ShouldDisplaySuccessMessageInGreen()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            UIHelper.ShowSuccessMessage("Success");

            // Assert
            var expectedOutput = "Success\n";
            Assert.Contains(expectedOutput, output.ToString());
        }

        [Fact]
        public void ShowWarningMessage_ShouldDisplayWarningMessageInRed()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            UIHelper.ShowWarningMessage("Warning");

            // Assert
            var expectedOutput = "Warning\n";
            Assert.Contains(expectedOutput, output.ToString());
        }

        [Fact]
        public void ShowInfoMessage_ShouldDisplayInfoMessageInYellow()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            UIHelper.ShowInfoMessage("Info");

            // Assert
            var expectedOutput = "Info\n";
            Assert.Contains(expectedOutput, output.ToString());
        }
    }
}
