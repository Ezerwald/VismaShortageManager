using VismaShortageManager.src.ConsoleApp.Helpers;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Interfaces;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.Services
{
    public class ShortageService
    {
        private readonly IShortageRepository _repository;

        public ShortageService(IShortageRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Adds a new shortage, checking for duplicates and handling priority.
        /// </summary>
        /// <param name="shortage">The shortage to add.</param>
        public void AddShortage(Shortage shortage)
        {
            try
            {
                var shortages = _repository.GetAllShortages();
                var existingShortage = GetExistingShortage(shortage, shortages);

                if (existingShortage != null)
                {
                    HandleExistingShortage(shortage, existingShortage, shortages);
                }
                else
                {
                    AddNewShortage(shortage, shortages);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the shortage: {ex.Message}");
            }
        }

        private Shortage? GetExistingShortage(Shortage shortage, List<Shortage> shortages)
        {
            return shortages.FirstOrDefault(s => s.Title == shortage.Title && s.Room == shortage.Room);
        }

        private void HandleExistingShortage(Shortage shortage, Shortage existingShortage, List<Shortage> shortages)
        {
            if (shortage.Priority > existingShortage.Priority)
            {
                shortages.Remove(existingShortage);
                shortages.Add(shortage);
                _repository.SaveShortages(shortages);
                UIHelper.ShowSuccessMessage("Shortage updated due to higher priority.");
            }
            else
            {
                UIHelper.ShowInfoMessage("Shortage already exists with equal or higher priority.");
            }
        }

        private void AddNewShortage(Shortage shortage, List<Shortage> shortages)
        {
            shortages.Add(shortage);
            _repository.SaveShortages(shortages);
            UIHelper.ShowSuccessMessage("Shortage added successfully.");
        }

        /// <summary>
        /// Deletes a shortage if the user is the creator or an administrator.
        /// </summary>
        /// <param name="title">The title of the shortage to delete.</param>
        /// <param name="room">The room of the shortage to delete.</param>
        /// <param name="user">The user attempting to delete the shortage.</param>
        public void DeleteShortage(string title, string room, User user)
        {
            try
            {
                var shortages = _repository.GetAllShortages();
                var shortage = shortages.FirstOrDefault(s => s.Title == title && s.Room.ToString() == room);

                if (shortage == null)
                {
                    UIHelper.ShowInfoMessage("Shortage not found.");
                    return;
                }

                if (IsUserAuthorizedToDelete(shortage, user))
                {
                    shortages.Remove(shortage);
                    _repository.SaveShortages(shortages);
                    UIHelper.ShowSuccessMessage("Shortage deleted successfully.");
                }
                else
                {
                    UIHelper.ShowWarningMessage("You do not have permission to delete this shortage.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the shortage: {ex.Message}");
            }
        }

        private bool IsUserAuthorizedToDelete(Shortage shortage, User user)
        {
            return shortage.CreatedBy == user.Name || user.IsAdministrator;
        }

        /// <summary>
        /// Lists shortages with optional filters. Administrators see all, others see only their own.
        /// </summary>
        /// <param name="user">The user requesting the list.</param>
        /// <param name="filterTitle">Optional title filter.</param>
        /// <param name="filterDateStart">Optional start date filter.</param>
        /// <param name="filterDateEnd">Optional end date filter.</param>
        /// <param name="filterCategory">Optional category filter.</param>
        /// <param name="filterRoom">Optional room filter.</param>
        /// <returns>A list of shortages matching the filters.</returns>
        public List<Shortage> ListShortages(User user, string? filterTitle = null, DateTime? filterDateStart = null, DateTime? filterDateEnd = null, CategoryType? filterCategory = null, RoomType? filterRoom = null)
        {
            try
            {
                var shortages = _repository.GetAllShortages();
                shortages = FilterShortagesByUser(shortages, user);

                if (!string.IsNullOrEmpty(filterTitle))
                {
                    shortages = shortages.Where(s => s.Title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (filterDateStart.HasValue)
                {
                    shortages = shortages.Where(s => s.CreatedOn >= filterDateStart.Value).ToList();
                }

                if (filterDateEnd.HasValue)
                {
                    shortages = shortages.Where(s => s.CreatedOn <= filterDateEnd.Value).ToList();
                }

                if (filterCategory.HasValue)
                {
                    shortages = shortages.Where(s => s.Category == filterCategory.Value).ToList();
                }

                if (filterRoom.HasValue)
                {
                    shortages = shortages.Where(s => s.Room == filterRoom.Value).ToList();
                }

                return shortages.OrderByDescending(s => s.Priority).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing the shortages: {ex.Message}");
                return new List<Shortage>();
            }
        }

        private List<Shortage> FilterShortagesByUser(List<Shortage> shortages, User user)
        {
            if (!user.IsAdministrator)
            {
                shortages = shortages.Where(s => s.CreatedBy == user.Name).ToList();
            }
            return shortages;
        }
    }
}
