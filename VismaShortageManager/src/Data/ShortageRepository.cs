using System.Text.Json;
using System.Text.Json.Serialization;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.Data
{
    public class ShortageRepository : IShortageRepository
    {
        private readonly string _filePath;

        public ShortageRepository(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Reads all shortages from the JSON file.
        /// </summary>
        /// <returns>A list of shortages.</returns>
        public List<Shortage>? GetAllShortages()
        {
            try
            {
                var jsonData = JsonFileHandler.ReadJsonFromFile(_filePath);
                return string.IsNullOrEmpty(jsonData) ? new List<Shortage>() : JsonSerializer.Deserialize<List<Shortage>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error reading shortages from file: {ex.Message}");
                return new List<Shortage>();
            }
        }

        /// <summary>
        /// Saves all shortages to the JSON file.
        /// </summary>
        /// <param name="shortages">The list of shortages to save.</param>
        public void SaveShortages(List<Shortage> shortages)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(shortages, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                });
                JsonFileHandler.WriteJsonToFile(_filePath, jsonData);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error writing shortages to file: {ex.Message}");
            }
        }
    }
}
