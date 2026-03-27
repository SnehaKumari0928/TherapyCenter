namespace TherapyCenter.Entities
{
    public class Appointment
    {
        public int Appointmentid {  get; set; }
        public int PatientId { get; set; }

        public Patient Patient { get; set; }



        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int TherapyId { get; set; }

        public Therapy Therapy { get; set; }

        public int? ReceptionistId { get; set; }
        public User? Receptionist { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime {  get; set; }
        public TimeSpan EndTime { get; set; }

        public string Status {  get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DoctorFinding? DoctorFinding { get; set; }
        public Payment? Payment { get; set;  }

    }
}
