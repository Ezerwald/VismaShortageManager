namespace VismaShortageManager.src.Domain.Interfaces
{
    public interface IInputParser
    {
        T ParseEnum<T>(string prompt = null) where T : struct, Enum;
        int ParseIntInRange(string prompt, int min, int max);
        string ParseNonEmptyString(string prompt);
        string ParseAnyString(string prompt);
        bool ParseBool(string prompt);
        DateTime? ParseDateTime(string prompt);
    }
}
