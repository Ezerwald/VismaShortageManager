using System;
using System.Collections.Generic;
using System.IO;
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
        public List<Shortage> GetAllShortages()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Shortage>();
                }

                var jsonData = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Shortage>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                }) ?? new List<Shortage>();
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
                File.WriteAllText(_filePath, jsonData);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error writing shortages to file: {ex.Message}");
            }
        }
    }
}
