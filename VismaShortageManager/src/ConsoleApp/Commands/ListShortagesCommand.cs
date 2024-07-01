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
        private User _currentUser;

        private string? _filterTitle;
        private DateTime? _filterDateStart;
        private DateTime? _filterDateEnd;
        private CategoryType? _filterCategory;
        private RoomType? _filterRoom;

        public ListShortagesCommand(ShortageService shortageService, DeleteShortageCommand deleteShortageCommand)
        {
            _shortageService = shortageService;
            _deleteShortageCommand = deleteShortageCommand;
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

        private void ShowCurrentFilters()
        {
            Console.WriteLine("Current filters:");
            Console.WriteLine($"Title: {_filterTitle ?? "None"}");
            Console.WriteLine($"Date Start: {_filterDateStart?.ToString("yyyy-MM-dd") ?? "None"}");
            Console.WriteLine($"Date End: {_filterDateEnd?.ToString("yyyy-MM-dd") ?? "None"}");
            Console.WriteLine($"Category: {_filterCategory?.ToString() ?? "None"}");
            Console.WriteLine($"Room: {_filterRoom?.ToString() ?? "None"}");
            Console.WriteLine();
        }

        private void AddFilter()
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
                        _filterTitle = InputParser.ParseAnyString("Enter filter title:");
                        break;
                    case 2:
                        _filterDateStart = InputParser.ParseDateTime("Enter start date (yyyy-mm-dd):");
                        _filterDateEnd = InputParser.ParseDateTime("Enter end date (yyyy-mm-dd):");
                        break;
                    case 3:
                        _filterCategory = InputParser.ParseEnum<CategoryType>();
                        break;
                    case 4:
                        _filterRoom = InputParser.ParseEnum<RoomType>();
                        break;
                    default:
                        UIHelper.ShowInvalidInputResponse();
                        break; ;
                }
            });

        }

        private void ClearFilters()
        {
            UIHelper.SeparateMessage();
            _filterTitle = null;
            _filterDateStart = null;
            _filterDateEnd = null;
            _filterCategory = null;
            _filterRoom = null;
            Console.WriteLine("Filters cleared.");
        }

        private void ShowResults()
        {
            try
            {
                var shortages = _shortageService.ListShortages(
                    _currentUser,
                    _filterTitle,
                    _filterDateStart,
                    _filterDateEnd,
                    _filterCategory,
                    _filterRoom);

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
                    case 3:
                        UIHelper.ShowInvalidInputResponse();
                        break;
                }
            });
        }
    }
}
