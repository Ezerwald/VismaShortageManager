using VismaShortageManager.src.Domain.Interfaces;

namespace VismaShortageManager.src.ConsoleApp.Helpers
{

    public class InputParser : IInputParser
    {
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

                if (int.TryParse(Console.ReadLine(), out int selectedIndex) &&
                    selectedIndex >= 1 && selectedIndex <= enumValues.Length)
                {
                    return (T)enumValues.GetValue(selectedIndex - 1);
                }

                Console.WriteLine("Invalid selection. Please try again.");
            }
        }

        public int ParseIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }

        public string ParseNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }

        public string ParseAnyString(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public bool ParseBool(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt + " (yes/no)");
                var input = Console.ReadLine().ToLower();
                if (input == "yes")
                {
                    return true;
                }
                if (input == "no")
                {
                    return false;
                }

                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }
        }

        public DateTime? ParseDateTime(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
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
