using VismaShortageManager.src.Domain.Interfaces;

namespace VismaShortageManager.src.ConsoleApp.Helpers
{
    public class InputParser : IInputParser
    {
        public T ParseEnum<T>(string prompt = null) where T : struct, Enum
        {
            return PromptLoop(prompt, TryParseEnum<T>);
        }

        public int ParseIntInRange(string prompt, int min, int max)
        {
            return PromptLoop(prompt, input => TryParseIntInRange(input, min, max));
        }

        public string ParseNonEmptyString(string prompt)
        {
            return PromptLoop(prompt, TryParseNonEmptyString);
        }

        public string ParseAnyString(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public bool ParseBool(string prompt)
        {
            return PromptLoop(prompt, TryParseBool);
        }

        public DateTime? ParseDateTime(string prompt)
        {
            return PromptLoop(prompt, TryParseDateTime);
        }

        private T PromptLoop<T>(string prompt, Func<string, (bool isValid, T result)> tryParse)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                var (isValid, result) = tryParse(input);
                if (isValid) return result;

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        private (bool isValid, T result) TryParseEnum<T>(string input) where T : struct, Enum
        {
            if (Enum.TryParse<T>(input, out var result) && Enum.IsDefined(typeof(T), result))
            {
                return (true, result);
            }
            return (false, default);
        }

        private (bool isValid, int result) TryParseIntInRange(string input, int min, int max)
        {
            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return (true, value);
            }
            return (false, default);
        }

        private (bool isValid, string result) TryParseNonEmptyString(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return (true, input);
            }
            return (false, default);
        }

        private (bool isValid, bool result) TryParseBool(string input)
        {
            var loweredInput = input.ToLower();
            if (loweredInput == "yes" || loweredInput == "true" || loweredInput == "y")
            {
                return (true, true);
            }
            else if (loweredInput == "no" || loweredInput == "false" || loweredInput == "n")
            {
                return (true, false);
            }
            return (false, default);
        }

        private (bool isValid, DateTime? result) TryParseDateTime(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return (true, null);
            }

            if (DateTime.TryParse(input, out var dateTime))
            {
                return (true, dateTime);
            }
            return (false, default);
        }
    }
}
