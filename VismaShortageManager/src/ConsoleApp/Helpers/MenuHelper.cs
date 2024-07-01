namespace VismaShortageManager.src.ConsoleApp.Helpers
{
    public class MenuHelper
    {
        /// <summary>
        /// Displays a menu with several options.
        /// </summary>
        /// <param name="title">The menu's title to display.</param>
        /// <param name="options">List of options to display.</param>
        /// <param name="onSelect">Switch method that will be executed after choosing option.</param>
        public static void ShowMenu(string title, List<string> options, Action<int> onSelect)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            int choice = InputParserMethods.ParseIntInRange("", 1, options.Count);
            UIHelper.SeparateMessage();
            onSelect(choice);
        }
    }
}
