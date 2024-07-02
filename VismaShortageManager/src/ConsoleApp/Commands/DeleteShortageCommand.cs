using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.ConsoleApp.Commands
{
    public class DeleteShortageCommand : IConsoleCommand
    {
        private readonly IShortageService _shortageService;
        private readonly IInputParser _inputParser;
        private User _currentUser;

        public DeleteShortageCommand(IShortageService shortageService, IInputParser inputParser)
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
                var (title, room) = GetShortageDetails();
                _shortageService.DeleteShortage(title, room, _currentUser);
                UIHelper.SeparateMessage();
                ShowPostActionMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private (string title, string room) GetShortageDetails()
        {
            var title = _inputParser.ParseNonEmptyString("Enter title of the shortage to delete:");
            var room = _inputParser.ParseEnum<RoomType>().ToString();
            return (title, room);
        }

        private void ShowPostActionMenu()
        {
            bool shouldContinue = true;

            while (shouldContinue)
            {
                MenuHelper.ShowMenu("", new List<string>
                {
                    "Delete one more shortage",
                    "Back to menu"
                }, choice =>
                {
                    switch (choice)
                    {
                        case 1:
                            DeleteOneMoreShortage();
                            break;
                        case 2:
                            shouldContinue = false;
                            break;
                        default:
                            UIHelper.ShowInvalidInputResponse();
                            break;
                    }
                });
            }
        }
        private void DeleteOneMoreShortage()
        {
            try
            {
                var (title, room) = GetShortageDetails();
                _shortageService.DeleteShortage(title, room, _currentUser);
                UIHelper.SeparateMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
