using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.ConsoleApp.Commands
{
    public class AddShortageCommand : IConsoleCommand
    {
        private readonly IShortageService _shortageService;
        private readonly IInputParser _inputParser;
        private User _currentUser;

        public AddShortageCommand(IShortageService shortageService, IInputParser inputParser)
        {
            _shortageService = shortageService;
            _inputParser = inputParser;
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
            var title = _inputParser.ParseNonEmptyString("Enter title:");
            var room = _inputParser.ParseEnum<RoomType>();
            var category = _inputParser.ParseEnum<CategoryType>();
            var priority = _inputParser.ParseIntInRange("Enter priority (1-10):", 1, 10);

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
                            RegisterOneMoreShortage();
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
        private void RegisterOneMoreShortage()
        {
            try
            {
                var shortage = CreateShortage();
                _shortageService.AddShortage(shortage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
