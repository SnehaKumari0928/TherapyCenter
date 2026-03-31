namespace TherapyCenter.DTOs.PatientDTOs
{
    public class CreatePatientDto
    {
        public int GuardianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
