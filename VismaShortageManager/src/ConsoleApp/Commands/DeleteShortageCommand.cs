using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;

namespace VismaShortageManager.src.ConsoleApp.Commands
{
    public class DeleteShortageCommand
    {
        private readonly ShortageService _shortageService;
        private readonly User _currentUser;

        public DeleteShortageCommand(ShortageService shortageService, User currentUser)
        {
            _shortageService = shortageService;
            _currentUser = currentUser;
        }

        public void Execute()
        {
            var title = InputParser.ParseNonEmptyString("Enter title of the shortage to delete:");
            var room = InputParser.ParseEnum<RoomType>().ToString();

            _shortageService.DeleteShortage(title, room, _currentUser);
            Console.WriteLine("Shortage deleted successfully.");

            ShowPostActionMenu();
        }

        private void ShowPostActionMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Delete one more shortage");
                Console.WriteLine("2. Back to main menu");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Execute();
                        return;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}
