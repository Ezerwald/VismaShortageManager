using VismaShortageManager.src.Domain.Interfaces;

namespace VismaShortageManager.src.ConsoleApp.Helpers
{
    public class InputParser : IInputParser
    {
        /// <summary>
        /// Prompts the user for a valid enum value by presenting a numbered list of options.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="prompt">Optional prompt message to display.</param>
        /// <returns>The selected enum value.</returns>
        public T ParseEnum<T>(string? prompt = null) where T : struct, Enum
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(prompt))
                {
                    Console.WriteLine(prompt);
                }
                Console.WriteLine($"Select {typeof(T).Name}:");

                var enumValues = Enum.GetValues(typeof(T));
                for (int i = 0; i < enumValues.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {enumValues.GetValue(i)}");
                }

                if (int.TryParse(Console.ReadLine().Trim(), out int selectedIndex) &&
                    selectedIndex >= 1 && selectedIndex <= enumValues.Length)
                {
                    return (T)enumValues.GetValue(selectedIndex - 1);
                }

                Console.WriteLine("Invalid selection. Please try again.");
            }
        }

        /// <summary>
        /// Prompts the user for an integer within a specified range.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <param name="min">The minimum valid value.</param>
        /// <param name="max">The maximum valid value.</param>
        /// <returns>The entered integer.</returns>
        public int ParseIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine().Trim(), out int value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }

        /// <summary>
        /// Prompts the user for a non-empty string.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered string.</returns>
        public string ParseNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine().Trim();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }

        /// <summary>
        /// Prompts the user for any string, including an empty one.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered string.</returns>
        public string ParseAnyString(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine().Trim();
        }

        /// <summary>
        /// Prompts the user for a boolean value (yes/no).
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered boolean value.</returns>
        public bool ParseBool(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt + " (yes/no)");
                var input = Console.ReadLine().Trim().ToLower();
                if (input == "yes" || input == "true" || input == "y")
                {
                    return true;
                }
                if (input == "no" || input == "false" || input == "n")
                {
                    return false;
                }

                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }
        }

        /// <summary>
        /// Prompts the user for a valid date.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>The entered DateTime value, or null if the input is left empty.</returns>
        public DateTime? ParseDateTime(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                if (DateTime.TryParse(input, out var dateTime))
                {
                    return dateTime;
                }

                Console.WriteLine("Invalid date format. Please enter the date in the format yyyy-mm-dd.");
            }
        }
    }
}

