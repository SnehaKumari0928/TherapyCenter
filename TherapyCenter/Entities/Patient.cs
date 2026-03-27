namespace TherapyCenter.Entities
{
    public class Patient
    {
        public int PatientId {  get; set; }
        public int? GuardianId { get; set; }
        public User? Guardian { get; set; }

        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MedicalHistory{  get; set; }
        public DateTime CreatedTime { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
