
namespace Entities
{
    public class PatientProgress
    {
        public int PatientProgressId { get; set; }
        public Patient? Patient { get; set; }
        public string? ProgressDescription { get; set; }
        public string? Recommendation { get; set; }
        public DateTime DateOfAttention { get; set; }
        public string? HourOffAttention { get; set; }
        public int? SessionNumber { get; set; }
        public string? Archive { get; set; }
        public bool IsAttention { get; set; }
        public string? SystemHour { get; set; }
        public bool IsQueueRemoval { get; set; }
        public Employeed? Employeed { get; set; }
        public int? TimeDemoration { get; set; }
        public bool IsFlag { get; set; }
        public DateTime DateFlag { get; set; }
        public TypeAttention? TypeAttention { get; set; }
        public string? State { get; set; }
        public string? Time { get; set; }
    }
}
