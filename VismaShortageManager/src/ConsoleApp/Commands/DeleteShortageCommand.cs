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
                        "1. Delete one more shortage",
                        "2. Back to menu"
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
                        UIHelper.ShowInvalidInputResponse();
                        break;
                }
            }
        }
    }
}
