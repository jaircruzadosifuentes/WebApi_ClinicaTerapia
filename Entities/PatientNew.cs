 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientNew
    {
        public int PatientNewId { get; set; }
        public string? Names { get; set; }
        public string? SurNames { get; set; }
        public DateTime BirthDate { get; set; }
        public Document? Document { get; set; }
        public string? NroDocument { get; set; }
        public DateTime ReservedDay { get; set; }
        public string? HourInitial { get; set; }
        public string? HourFinished { get; set; }
        public Employeed? Employeed { get; set; }
        public int TimeInAttention { get; set; }
        public DateTime CreateadAt { get; set; }
        public bool SaveInDraft { get; set; }
        public string? CellPhone { get; set; }
        public string? Email { get; set; }
        public string? Reason { get; set; }
        public int Age { get; set; }
        public string? DisabilityDescription { get; set; }
        public string? ProfilePicture { get; set; }
        public decimal Weight { get; set; }
        public string? Disease { get; set; }
        public bool HasDisease { get; set; }
        public bool HasOperation { get; set; }
        public string? DescOperation { get; set; }
        public string? PhysicalExploration { get; set; }
        public int ShadowPain { get; set; }
        public string? Diagnosis { get; set; }
        public bool TakeMedicine { get; set; }
        public string? DescMedicine { get; set; }
        public PacketsOrUnitSessions? PacketsOrUnitSessions { get; set; }
        public int FrecuencyId { get; set; }
        public string? FrecuencyDesc { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? State { get; set; }
        public bool GenerateSchedule { get; set; }
        public string? Gender { get; set; }

    }
}
