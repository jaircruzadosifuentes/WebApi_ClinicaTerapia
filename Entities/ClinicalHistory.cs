using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ClinicalHistory
    {
        public int ClinicalHistoryId { get; set; }
        public decimal Weight { get; set; }
        public string? Disease { get; set; }
        public string? DescriptionOperation { get; set; }
        public string? PhysicalExploration { get; set; }
        public int ShadowPain { get; set; }
        public string? DescriptionDiagnostica { get; set; }
        public string? DescriptionMedic { get; set; }
        public string? InformationAdditional { get; set; }
        public bool TakeMedicina { get; set; }
        public bool HasDisease { get; set; }
        public bool HasOperation { get; set; }
        public int FrecuencyId { get; set; }
        public Frecuency? Frecuency { get; set; }
        public PacketsOrUnitSessions? PacketsOrUnitSessions { get; set; }

    }
}
