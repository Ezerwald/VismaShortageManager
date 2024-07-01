using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;

namespace VismaShortageManager.src.ConsoleApp.Commands
{
    public class DeleteShortageCommand
    {
        private readonly ShortageService _shortageService;
        private User _currentUser;

        public DeleteShortageCommand(ShortageService shortageService)
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
            var title = InputParser.ParseNonEmptyString("Enter title of the shortage to delete:");
            var room = InputParser.ParseEnum<RoomType>().ToString();
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
                            Execute();
                            shouldContinue = false;
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
    }
}