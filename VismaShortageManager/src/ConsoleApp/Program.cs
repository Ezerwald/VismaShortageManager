using Microsoft.Extensions.DependencyInjection;
using VismaShortageManager.src.ConsoleApp;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Data;
using VismaShortageManager.src.ConsoleApp.Commands;
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
                .AddTransient<ShortageService>()
                .AddTransient<ConsoleApp>()
                .AddCommands()
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
