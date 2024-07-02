using VismaShortageManager.src.ConsoleApp.Commands;
using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.ConsoleApp
{
    public class ConsoleApp
    {
        private readonly AddShortageCommand _addShortageCommand;
        private readonly DeleteShortageCommand _deleteShortageCommand;
        private readonly ListShortagesCommand _listShortagesCommand;
        private User _currentUser;

        public ConsoleApp(
            AddShortageCommand addShortageCommand,
            DeleteShortageCommand deleteShortageCommand,
            ListShortagesCommand listShortagesCommand
            )
        {
            _addShortageCommand = addShortageCommand;
            _deleteShortageCommand = deleteShortageCommand;
            _listShortagesCommand = listShortagesCommand;
        }

        public void Run()
        {
            Login();
            ShowMainMenu();
        }

        private void Login()
        {
            var name = InputParserMethods.ParseNonEmptyString("Enter your name:");
            var isAdmin = InputParserMethods.ParseBool("Are you an administrator?");

            _currentUser = new User
            {
                Name = name,
                IsAdministrator = isAdmin
            };

            SetUserToCommands(_currentUser);

            Console.Clear();
        }

        private void SetUserToCommands(User user)
        {
            _addShortageCommand.SetUser(user);
            _deleteShortageCommand.SetUser(user);
            _listShortagesCommand.SetUser(user);
        }

        private void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Username: {_currentUser.Name}   Administrator status: {_currentUser.IsAdministrator}");
                MenuHelper.ShowMenu("Main Menu:", new List<string>
                {
                    "Register new shortage",
                    "Delete shortage",
                    "List all shortages",
                    "Exit"
                }, choice =>
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            _addShortageCommand.Execute();
                            break;
                        case 2:
                            Console.Clear();
                            _deleteShortageCommand.Execute();
                            break;
                        case 3:
                            Console.Clear();
                            _listShortagesCommand.Execute();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                });
            }
        }
    }
}
