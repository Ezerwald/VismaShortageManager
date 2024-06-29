using Microsoft.Extensions.DependencyInjection;
using VismaShortageManager.src.Data;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Services;

namespace VismaShortageManager.src.ConsoleApp
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
                .BuildServiceProvider();

            // Run the application
            var app = serviceProvider.GetService<ConsoleApp>();
            app.Run();
        }
    }
}