using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Option
    {
        public int OptionId { get; set; }
        public string? Component { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public bool State { get; set; }
        public Employeed? Employeed { get; set; }
        public List<OptionItems> Items { get; set; }
        public string? NameItem { get; set; }
        public int OptionItemId { get; set; }

        public Option() 
        { 
            Items = new List<OptionItems>();
        }

    }
}
