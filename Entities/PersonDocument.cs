using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonDocument
    {
        public int PersonDocumentId { get; set; }
        public string? NroDocument { get; set; }
        public Document? Document { get; set; }
    }
}
