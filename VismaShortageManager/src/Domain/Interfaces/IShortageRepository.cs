using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.Domain.Interfaces
{
    public interface IShortageRepository
    {
        List<Shortage>? GetAllShortages();
        void SaveShortages(List<Shortage> shortages);
    }
}
