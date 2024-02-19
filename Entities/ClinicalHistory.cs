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
        public string? BucketName { get; set; }
        public string? BucketFileName { get; set; }
        public Patient? Patient { get; set; }
        public Employeed? Employeed { get; set; }
        public string? NameFileHistoryClinic { get; set; }
        public string? NameFileHistoryClinicTmp { get; set; }
        public string? State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Terapeuta { get; set; }
        public string? NroClinicHistory { get; set; }
        public decimal? HeightOfPerson { get; set; }
        public decimal? Imc { get; set; }
        public string? Title { get; set; }


    }
}
