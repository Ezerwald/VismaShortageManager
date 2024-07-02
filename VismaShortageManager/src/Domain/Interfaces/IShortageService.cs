using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaShortageManager.src.Domain.Enums;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.Domain.Interfaces
{
    public interface IShortageService
    {
        void AddShortage(Shortage shortage);
        public void DeleteShortage(string title, string room, User user);
        List<Shortage> ListShortages(User user, string? filterTitle = null, DateTime? filterDateStart = null, DateTime? filterDateEnd = null, CategoryType? filterCategory = null, RoomType? filterRoom = null);
    }
}
