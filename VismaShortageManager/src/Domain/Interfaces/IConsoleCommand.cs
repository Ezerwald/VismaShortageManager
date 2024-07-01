using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaShortageManager.src.Domain.Models;

namespace VismaShortageManager.src.Domain.Interfaces
{
    public interface IConsoleCommand
    {
        void SetUser(User user);
        void Execute();
    }
}
