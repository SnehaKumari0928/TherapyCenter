namespace TherapyCenter2.DTOs.Appointment
{
    public class WalkInAppointmentDto
    {
        public int DoctorId { get; set; }

        public int TherapyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }


        public string? Notes { get; set; }
    }
}
