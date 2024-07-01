using Microsoft.Extensions.DependencyInjection;
using VismaShortageManager.src.Domain.Interfaces;

namespace VismaShortageManager.src.ConsoleApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            var commandTypes = typeof(Program).Assembly.GetTypes()
                .Where(t => typeof(IConsoleCommand).IsAssignableFrom(t) && !t.IsInterface);

            foreach (var commandType in commandTypes)
            {
                services.AddTransient(commandType);
            }

            return services;
        }
    }
}
