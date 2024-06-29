using VismaShortageManager.src.Services;
using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.ConsoleApp
{
    public class ConsoleApp
    {
        private readonly ShortageService _shortageService;
        private User _currentUser;

        public ConsoleApp(ShortageService shortageService)
        {
            _shortageService = shortageService;
        }

        public void Run()
        {
            Login();
            ShowMainMenu();
        }

        private void Login()
        {
            var name = InputParser.ParseNonEmptyString("Enter your name:");
            var isAdmin = InputParser.ParseBool("Are you an administrator?");

            _currentUser = new User
            {
                Name = name,
                IsAdministrator = isAdmin
            };
            
            Console.Clear();
        }

        private void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Username: {_currentUser.Name}   Administrator status: {_currentUser.IsAdministrator}");
                UIHelper.ShowOptionsMenu(
                    title: "Main Menu:",
                    options: new List<string>
                    {
                        "1. Register new shortage",
                        "2. Delete shortage",
                        "3. List all requests",
                        "4. Exit"
                    }
                );

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        new AddShortageCommand(_shortageService, _currentUser).Execute();
                        break;
                    case "2":
                        Console.Clear();
                        new DeleteShortageCommand(_shortageService, _currentUser).Execute();
                        break;
                    case "3":
                        new ListShortagesCommand(_shortageService, _currentUser).Execute();
                        break;
                    case "4":
                        return;
                    default:
                        UIHelper.ShowInvalidInputResponse();
                        break;
                }
            }
        }
    }
}
