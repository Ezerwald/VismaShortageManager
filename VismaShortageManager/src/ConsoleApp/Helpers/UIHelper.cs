using System;

namespace VismaShortageManager.src.ConsoleApp.Helpers
{
    public static class UIHelper
    {
        /// <summary>
        /// Displays a separator line with a customizable character and length.
        /// </summary>
        /// <param name="character">The character to use for the separator line.</param>
        /// <param name="length">The length of the separator line.</param>
        public static void SeparateMessage(char character = '-', int length = 20)
        {
            Console.WriteLine(new string(character, length));
        }

        /// <summary>
        /// Displays a standard invalid input response and waits for a key press.
        /// </summary>
        /// <param name="message">Optional custom message for invalid input.</param>
        public static void ShowInvalidInputResponse(string message = "Invalid option, please try again")
        {
            ShowWarningMessage(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Displays a success message.
        /// </summary>
        /// <param name="message">The success message to display.</param>
        public static void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a warning message.
        /// </summary>
        /// <param name="message">The warning message to display.</param>
        public static void ShowWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an informational message.
        /// </summary>
        /// <param name="message">The informational message to display.</param>
        public static void ShowInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays menu with several options.
        /// </summary>
        /// <param name="options">List of options to display.</param>
        /// <param name="message">The informational message to display.</param>
        public static void ShowOptions(List<string> options, string message = "")
        {
            var i = 1;

            Console.WriteLine(message);

            foreach (string option in options)
            {
                Console.WriteLine($"{i}. {option}");
                i++;
            }
        }

    }
}
