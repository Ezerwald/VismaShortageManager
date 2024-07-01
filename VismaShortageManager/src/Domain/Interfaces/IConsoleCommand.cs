using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.Domain.Interfaces
{
    public interface IConsoleCommand
    {
        void SetUser(User user);
        void Execute();
    }
}
