using System.ComponentModel.DataAnnotations;

namespace TherapyCenter2.DTOs.Appointment
{
    public class AppointmentCreateDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int TherapyId { get; set; }

        [Required]
        public int SlotId { get; set; }

        public int? ReceptionistId { get; set; }

        public string? Notes { get; set; }
    }
}
