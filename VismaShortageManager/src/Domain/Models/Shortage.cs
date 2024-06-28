using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VismaShortageManager.src.Domain.Enums;

namespace VismaShortageManager.src.Domain.Models
{
    public class Shortage
    {
        public required string Title { get; set; }  // Title of shortage request
        public RoomType Room { get; set; }  // Room where shortage appear
        public CategoryType Category { get; set; }  // Category of shortage request
        public int Priority { get; set; }   // Priority of request
        public DateTime CreatedOn { get; set; } // Track the date of request creation
        public required string CreatedBy { get; set; }  // Track the creator of request
    }
}