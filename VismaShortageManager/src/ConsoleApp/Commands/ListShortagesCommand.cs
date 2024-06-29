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
    public class ListShortagesCommand
    {
        private readonly ShortageService _shortageService;
        private readonly User _currentUser;

        private string? _filterTitle;
        private DateTime? _filterDateStart;
        private DateTime? _filterDateEnd;
        private CategoryType? _filterCategory;
        private RoomType? _filterRoom;

        public ListShortagesCommand(ShortageService shortageService, User currentUser)
        {
            _shortageService = shortageService;
            _currentUser = currentUser;
        }

        public void Execute()
        {
            while (true)
            {
                ShowPreActionMenu();

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddFilter();
                        break;
                    case "2":
                        ClearFilters();
                        break;
                    case "3":
                        ShowResults();
                        break;
                    case "4":
                        return;
                    default:
                        UIHelper.ShowInvalidInputResponse();
                        break;
                }
            }
        }

        private void ShowPreActionMenu()
        {
            Console.Clear();
            ShowCurrentFilters();
            Console.WriteLine("1. Add filter");
            Console.WriteLine("2. Clear filters");
            Console.WriteLine("3. Show results");
            Console.WriteLine("4. Back to main menu");
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
            UIHelper.SeparateMessage();
            Console.WriteLine("Select filter to add:");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. CreatedOn Date Range");
            Console.WriteLine("3. Category");
            Console.WriteLine("4. Room");
            var filterChoice = Console.ReadLine();

            switch (filterChoice)
            {
                case "1":
                    _filterTitle = InputParser.ParseAnyString("Enter filter title:");
                    break;
                case "2":
                    _filterDateStart = InputParser.ParseDateTime("Enter start date (yyyy-mm-dd):");
                    _filterDateEnd = InputParser.ParseDateTime("Enter end date (yyyy-mm-dd):");
                    break;
                case "3":
                    _filterCategory = InputParser.ParseEnum<CategoryType>();
                    break;
                case "4":
                    _filterRoom = InputParser.ParseEnum<RoomType>();
                    break;
                default:
                    UIHelper.ShowInvalidInputResponse();
                    break;
            }
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
            var shortages = _shortageService.ListShortages(
                _currentUser,
                _filterTitle,
                _filterDateStart,
                _filterDateEnd,
                _filterCategory,
                _filterRoom);

            UIHelper.SeparateMessage();
            Console.WriteLine("Shortages:");
            foreach (var shortage in shortages)
            {
                Console.WriteLine($"{shortage.Priority}: {shortage.Title} - {shortage.Room} - {shortage.Category} - Created by: {shortage.CreatedBy} on {shortage.CreatedOn}");
            }

            UIHelper.SeparateMessage();
            Console.WriteLine("1. Delete a shortage");
            Console.WriteLine("2. Back to filter menu");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    new DeleteShortageCommand(_shortageService, _currentUser).Execute();
                    break;
                case "2":
                    return;
                default:
                    UIHelper.ShowInvalidInputResponse();
                    break;
            }
        }
    }
}