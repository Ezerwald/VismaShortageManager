using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Console.WriteLine($"Welcome, {_currentUser.Name}. Administrator status: {_currentUser.IsAdministrator}");
        }

        private void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Register new shortage");
                Console.WriteLine("2. Delete shortage");
                Console.WriteLine("3. List all requests");
                Console.WriteLine("4. Exit");

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
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}
