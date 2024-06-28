using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaShortageManager.src.Domain.Enums;

public class Shortage
{
    public string Title { get; set; }
    public string Name { get; set; }
    public RoomType Room { get; set; }
    public CategoryType Category { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedOn { get; set; }
}
