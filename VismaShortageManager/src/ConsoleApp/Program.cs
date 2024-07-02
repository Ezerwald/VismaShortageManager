using Microsoft.Extensions.DependencyInjection;
using VismaShortageManager.src.ConsoleApp;
using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Data;
using VismaShortageManager.src.Services;

namespace VismaShortageManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Setup Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IShortageRepository>(new ShortageRepository("shortages.json"))
                .AddTransient<IShortageService, ShortageService>()
                .AddTransient<IInputParser, InputParser>() // Register IInputParser
                .AddCommands() // Register all commands dynamically
                .AddTransient<ConsoleApp>() // Ensure ConsoleApp is registered
                .BuildServiceProvider();

            // Run the application
            var app = serviceProvider.GetService<ConsoleApp>();
            if (app != null)
            {
                try
                {
                    app.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Failed to initialize the application.");
            }
        }
    }
}
