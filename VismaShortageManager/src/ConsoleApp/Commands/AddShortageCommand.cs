using System.Windows.Input;
using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;
using VismaShortageManager.src.Domain.Interfaces;

namespace VismaShortageManager.src.ConsoleApp.Commands
{
    public class AddShortageCommand : IConsoleCommand
    {
        private readonly ShortageService _shortageService;
        private User _currentUser;

        public AddShortageCommand(ShortageService shortageService)
        {
            _shortageService = shortageService;
        }

        public void SetUser(User user)
        {
            _currentUser = user;
        }

        public void Execute()
        {
            try
            {
                var shortage = CreateShortage();
                _shortageService.AddShortage(shortage);
                UIHelper.SeparateMessage();
                ShowPostActionMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private Shortage CreateShortage()
        {
            var title = InputParser.ParseNonEmptyString("Enter title:");
            var room = InputParser.ParseEnum<RoomType>();
            var category = InputParser.ParseEnum<CategoryType>();
            var priority = InputParser.ParseIntInRange("Enter priority (1-10):", 1, 10);

            return new Shortage
            {
                Title = title,
                Room = room,
                Category = category,
                Priority = priority,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUser.Name
            };
        }

        private void ShowPostActionMenu()
        {
            bool shouldContinue = true;

            while (shouldContinue)
            {
                MenuHelper.ShowMenu("", new List<string>
                {
                    "Register one more shortage",
                    "Back to main menu"
                }, choice =>
                {
                    switch (choice)
                    {
                        case 1:
                            Execute();
                            shouldContinue = false;
                            break;
                        case 2:
                            shouldContinue = false;
                            break;
                        default:
                            UIHelper.ShowInvalidInputResponse("Invalid option, please try again.");
                            break;
                    }
                });
            }
        }
    }
}