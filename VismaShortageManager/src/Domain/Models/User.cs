using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaShortageManager.src.Domain.Models
{
    public class User
    {
        public required string Name { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
