using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class QuizDetail
    {
        public int QuizDetailId { get; set; }
        public Quiz? Quiz { get; set; }
        public string? Name { get; set; }
        public string? TypeOfQuestion { get; set; }
        public int? NroQuestion { get; set; }

    }
}
