using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public Person? Person { get; set; }
        public string? PatientCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModificationDate { get; set; }
        public PatientState? PatientState { get; set; }
        public List<PatientProgress> PatientProgresses { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? NumberDocument { get; set; }
        public string? Operator { get; set; }
        public PatientNew? PatientNew { get; set; }
        public DateTime DateInitial { get; set; }
        public DateTime DateFinished { get; set; }
        public decimal Percentaje { get; set; }
        public bool SaveInDraft { get; set; }
        public int Correlative { get; set; }
        public PatientSolicitude? PatientSolicitude { get; set; }
        public string? State { get; set; }
        public string? Reason { get; set; }
        public ClinicalHistory? ClinicalHistory { get; set; }
        public bool ScheduleGenerate { get; set; }
        public Pay? Pay { get; set; }
        public string? UserNamePatient { get; set; }
        public Patient()
        {
            PatientProgresses = new List<PatientProgress>();
        }
    }
}
