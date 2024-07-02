using VismaShortageManager.src.ConsoleApp.Helpers;

namespace VismaShortageManager.Tests.Helpers
{
    public class MenuHelperTests : IDisposable
    {
        private StringWriter _consoleOutput;
        private StringReader _consoleInput;

        public MenuHelperTests()
        {
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
        public void ShowMenu_ShouldDisplayMenuAndExecuteAction()
        {
            // Arrange
            var options = new List<string> { "Option 1", "Option 2", "Option 3" };
            var input = "2\n";
            _consoleInput = new StringReader(input); // Update input
            _consoleOutput = new StringWriter(); // Update output
            Console.SetIn(_consoleInput);
            Console.SetOut(_consoleOutput);

            var selectedOption = 2;
            void OnSelect(int choice) => selectedOption = choice;

            // Act
            MenuHelper.ShowMenu("Menu Title", options, OnSelect);

            // Assert
            var expectedOutput = @"Menu Title
1. Option 1
2. Option 2
3. Option 3

--------------------
";
            Assert.Equal(expectedOutput, _consoleOutput.ToString());
            Assert.Equal(2, selectedOption);
        }
    }
}
