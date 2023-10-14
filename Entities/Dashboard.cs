using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Dashboard
    {
        public int DashBoardId { get; set; }
        public int Size { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Url { get; set; }
    }
}
