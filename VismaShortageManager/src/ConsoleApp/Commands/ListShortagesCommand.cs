using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;
using VismaShortageManager.src.Services;

namespace VismaShortageManager.src.ConsoleApp.Commands
{
    public class ListShortagesCommand : IConsoleCommand
    {
        private readonly ShortageService _shortageService;
        private readonly DeleteShortageCommand _deleteShortageCommand;
        private readonly IInputParser _inputParser;
        private User _currentUser;

        public string? FilterTitle { get; private set; }
        public DateTime? FilterDateStart { get; private set; }
        public DateTime? FilterDateEnd { get; private set; }
        public CategoryType? FilterCategory { get; private set; }
        public RoomType? FilterRoom { get; private set; }

        public ListShortagesCommand(ShortageService shortageService, DeleteShortageCommand deleteShortageCommand, IInputParser inputParser)
        {
            _shortageService = shortageService;
            _deleteShortageCommand = deleteShortageCommand;
            _inputParser = inputParser;
        }

        public void SetUser(User user)
        {
            _currentUser = user;
            _deleteShortageCommand.SetUser(user);
        }

        public void Execute()
        {
            bool shouldContinue = true;
            while (shouldContinue)
            {
                Console.Clear();
                ShowCurrentFilters();
                MenuHelper.ShowMenu("Main Menu:", new List<string>
                {
                    "Add filter",
                    "Clear filters",
                    "Show results",
                    "Back to main menu"
                }, choice =>
                {
                    switch (choice)
                    {
                        case 1:
                            AddFilter();
                            break;
                        case 2:
                            ClearFilters();
                            break;
                        case 3:
                            ShowResults();
                            break;
                        case 4:
                            shouldContinue = false;
                            break;
                        default:
                            UIHelper.ShowInvalidInputResponse();
                            break;
                    }
                });
            }
        }

        public void ShowCurrentFilters()
        {
            Console.WriteLine("Current filters:");
            Console.WriteLine($"Title: {FilterTitle ?? "None"}");
            Console.WriteLine($"Date Start: {FilterDateStart?.ToString("yyyy-MM-dd") ?? "None"}");
            Console.WriteLine($"Date End: {FilterDateEnd?.ToString("yyyy-MM-dd") ?? "None"}");
            Console.WriteLine($"Category: {FilterCategory?.ToString() ?? "None"}");
            Console.WriteLine($"Room: {FilterRoom?.ToString() ?? "None"}");
            Console.WriteLine();
        }

        public void AddFilter()
        {
            MenuHelper.ShowMenu("Select filter to add:", new List<string>
            {
                "Title",
                "CreatedOn Date Range",
                "Category",
                "Room"
            }, filterChoice =>
            {
                switch (filterChoice)
                {
                    case 1:
                        FilterTitle = _inputParser.ParseAnyString("Enter filter title:");
                        break;
                    case 2:
                        FilterDateStart = _inputParser.ParseDateTime("Enter start date (yyyy-mm-dd):");
                        FilterDateEnd = _inputParser.ParseDateTime("Enter end date (yyyy-mm-dd):");
                        break;
                    case 3:
                        FilterCategory = _inputParser.ParseEnum<CategoryType>();
                        break;
                    case 4:
                        FilterRoom = _inputParser.ParseEnum<RoomType>();
                        break;
                    default:
                        UIHelper.ShowInvalidInputResponse();
                        break;
                }
            });
        }

        public void ClearFilters()
        {
            UIHelper.SeparateMessage();
            FilterTitle = null;
            FilterDateStart = null;
            FilterDateEnd = null;
            FilterCategory = null;
            FilterRoom = null;
            Console.WriteLine("Filters cleared.");
        }

        private void ShowResults()
        {
            try
            {
                var shortages = _shortageService.ListShortages(
                    _currentUser,
                    FilterTitle,
                    FilterDateStart,
                    FilterDateEnd,
                    FilterCategory,
                    FilterRoom);

                DisplayShortages(shortages);
                ShowPostActionMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void DisplayShortages(IEnumerable<Shortage> shortages)
        {
            Console.WriteLine("Shortages:");
            foreach (var shortage in shortages)
            {
                Console.WriteLine($"{shortage.Priority}: {shortage.Title} - {shortage.Room} - {shortage.Category} - Created by: {shortage.CreatedBy} on {shortage.CreatedOn}");
            }
        }

        private void ShowPostActionMenu()
        {
            UIHelper.SeparateMessage();
            MenuHelper.ShowMenu("", new List<string>
            {
                "Delete a shortage",
                "Back to filter menu",
            }, choice =>
            {
                switch (choice)
                {
                    case 1:
                        _deleteShortageCommand.Execute();
                        break;
                    case 2:
                        break;
                    default:
                        UIHelper.ShowInvalidInputResponse();
                        break;
                }
            });
        }
    }
}
