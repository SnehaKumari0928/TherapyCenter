using Microsoft.AspNetCore.Mvc;

namespace TherapyCenter.Entities
{
    public class DoctorFinding
    {
        public int FindingId { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public string? Observations { get; set; }
        public string? Recommendations {  get; set; }

        public DateTime? NextSessionDate {  get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
