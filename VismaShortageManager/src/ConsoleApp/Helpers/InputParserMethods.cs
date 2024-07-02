using VismaShortageManager.src.Domain.Interfaces;

namespace VismaShortageManager.src.ConsoleApp.Helpers
{
    public static class InputParserMethods
    {

        static readonly InputParser InputParser = new();

        /// <summary>
        /// Prompts the user for a valid enum value by presenting a numbered list of options.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="prompt">Optional prompt message to display.</param>
        /// <returns>The selected enum value.</returns>
        public static T ParseEnum<T>(string? prompt = null) where T : struct, Enum
        {
            return InputParser.ParseEnum<T>(prompt);
        }

        /// <summary>
        /// Prompts the user for an integer within a specified range.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="min">The minimum valid value.</param>
        /// <param name="max">The maximum valid value.</param>
        /// <returns>The entered integer.</returns>
        public static int ParseIntInRange(string prompt, int min, int max)
        {
            return InputParser.ParseIntInRange(prompt, min, max);
        }

        /// <summary>
        /// Prompts the user for a non-empty string.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered string.</returns>
        public static string ParseNonEmptyString(string prompt)
        {
            return InputParser.ParseNonEmptyString(prompt);
        }

        /// <summary>
        /// Prompts the user for any string, including an empty one.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered string.</returns>
        public static string ParseAnyString(string prompt)
        {
            return InputParser.ParseAnyString(prompt);
        }

        /// <summary>
        /// Prompts the user for a boolean value (yes/no).
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered boolean value.</returns>
        public static bool ParseBool(string prompt)
        {
            return InputParser.ParseBool(prompt);
        }

        /// <summary>
        /// Prompts the user for a valid date.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered DateTime value, or null if the input is left empty.</returns>
        public static DateTime? ParseDateTime(string prompt)
        {
           return InputParser.ParseDateTime(prompt);
        }
    }
}