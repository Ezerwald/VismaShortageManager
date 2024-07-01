using VismaShortageManager.src.ConsoleApp.Helpers;

namespace VismaShortageManager.Tests.Helpers
{
    public class MenuHelperTests
    {
        [Fact]
        public void ShowMenu_ShouldDisplayMenuAndExecuteAction()
        {
            // Arrange
            var options = new List<string> { "Option 1", "Option 2", "Option 3" };
            var input = "2\n";
            var inputReader = new StringReader(input);
            Console.SetIn(inputReader);
            var output = new StringWriter();
            Console.SetOut(output);

            var selectedOption = -1;
            void OnSelect(int choice) => selectedOption = choice;

            // Act
            MenuHelper.ShowMenu("Menu Title", options, OnSelect);

            // Assert
            var expectedOutput = @"Menu Title
1. Option 1
2. Option 2
3. Option 3
";
            Assert.Equal(expectedOutput, output.ToString());
            Assert.Equal(2, selectedOption);
        }
    }
}
