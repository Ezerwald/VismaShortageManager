using System;
using System.Collections.Generic;
using System.Linq;
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
            var shortages = _repository.GetAllShortages();
            var existingShortage = shortages.FirstOrDefault(s => s.Title == shortage.Title && s.Room == shortage.Room);

            if (existingShortage != null)
            {
                if (shortage.Priority > existingShortage.Priority)
                {
                    // Override the existing shortage with the new one if priority is higher
                    shortages.Remove(existingShortage);
                    shortages.Add(shortage);
                    _repository.SaveShortages(shortages);
                    Console.WriteLine("Shortage updated due to higher priority.");
                }
                else
                {
                    Console.WriteLine("Shortage already exists with equal or higher priority.");
                }
            }
            else
            {
                // Add new shortage
                shortages.Add(shortage);
                _repository.SaveShortages(shortages);
                Console.WriteLine("Shortage added successfully.");
            }
        }

        /// <summary>
        /// Deletes a shortage if the user is the creator or an administrator.
        /// </summary>
        /// <param name="title">The title of the shortage to delete.</param>
        /// <param name="room">The room of the shortage to delete.</param>
        /// <param name="user">The user attempting to delete the shortage.</param>
        public void DeleteShortage(string title, string room, User user)
        {
            var shortages = _repository.GetAllShortages();
            var shortage = shortages.FirstOrDefault(s => s.Title == title && s.Room.ToString() == room);

            if (shortage == null)
            {
                Console.WriteLine("Shortage not found.");
                return;
            }

            if (shortage.CreatedBy == user.Name || user.IsAdministrator)
            {
                shortages.Remove(shortage);
                _repository.SaveShortages(shortages);
                Console.WriteLine("Shortage deleted successfully.");
            }
            else
            {
                Console.WriteLine("You do not have permission to delete this shortage.");
            }
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
        public List<Shortage> ListShortages(User user, string filterTitle = null, DateTime? filterDateStart = null, DateTime? filterDateEnd = null, CategoryType? filterCategory = null, RoomType? filterRoom = null)
        {
            var shortages = _repository.GetAllShortages();

            if (!user.IsAdministrator)
            {
                shortages = shortages.Where(s => s.CreatedBy == user.Name).ToList();
            }

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
    }
}
