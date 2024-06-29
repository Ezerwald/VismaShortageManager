using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;

namespace VismaShortageManager.src.ConsoleApp.Commands
{
    public class AddShortageCommand
    {
        private readonly ShortageService _shortageService;
        private readonly User _currentUser;

        public AddShortageCommand(ShortageService shortageService, User currentUser)
        {
            _shortageService = shortageService;
            _currentUser = currentUser;
        }

        public void Execute()
        {
            var title = InputParser.ParseNonEmptyString("Enter title:");
            var room = InputParser.ParseEnum<RoomType>();
            var category = InputParser.ParseEnum<CategoryType>();
            var priority = InputParser.ParseIntInRange("Enter priority (1-10):", 1, 10);

            var shortage = new Shortage
            {
                Title = title,
                Room = room,
                Category = category,
                Priority = priority,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUser.Name
            };

            _shortageService.AddShortage(shortage);

            UIHelper.SeparateMessage();
            ShowPostActionMenu();
        }

        private void ShowPostActionMenu()
        {
            while (true)
            {
                UIHelper.ShowOptionsMenu(
                    new List<string>
                    {
                        "1. Register one more shortage",
                        "2. Back to main menu"
                    }
                );

                var choice = Console.ReadLine();
                UIHelper.SeparateMessage();
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
