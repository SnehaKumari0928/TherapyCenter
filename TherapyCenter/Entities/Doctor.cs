namespace TherapyCenter.Entities
{
    public class Doctor
    {
        public int DoctorId {  get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string? Specialization {  get; set; }
        public string? Bio { get; set; }
        public string? AvailableDays { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? DateTime { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        public ICollection<Slot>? Slots { get; set; }

    }
}
