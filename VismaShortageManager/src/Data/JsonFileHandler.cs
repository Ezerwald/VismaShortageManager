using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaShortageManager.src.Data
{
    public static class JsonFileHandler
    {
        /// <summary>
        /// Reads JSON data from a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>The JSON data as a string.</returns>
        public static string ReadJsonFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            try
            {
                var jsonData = File.ReadAllText(filePath);
                if (jsonData != null)
                {
                    return jsonData;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error reading file: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Writes JSON data to a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="jsonData">The JSON data to write.</param>
        public static void WriteJsonToFile(string filePath, string jsonData)
        {
            try
            {
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error writing to file: {ex.Message}");
                throw;
            }
        }
    }
}
