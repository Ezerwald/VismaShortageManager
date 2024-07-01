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
                Console.WriteLine($"File not found: {filePath}");
                return string.Empty;
            }

            try
            {
                return File.ReadAllText(filePath);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while reading file: {ioEx.Message}");
                throw;
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Console.WriteLine($"Access error: {uaEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
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
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while writing to file: {ioEx.Message}");
                throw;
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Console.WriteLine($"Access error: {uaEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
                throw;
            }
        }
    }
}
