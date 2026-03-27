namespace TherapyCenter.Entities
{
    public class User
    {
        public int UserId {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash {  get; set; }
        public string Role { get; set; }
        public string? PhoneNumber {  get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;



        public ICollection<Appointment>? ReceptionistAppointments { get; set; }
        public ICollection<Patient>? Guardians { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }

    }
}
