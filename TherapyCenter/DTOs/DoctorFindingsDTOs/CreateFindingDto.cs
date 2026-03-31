namespace TherapyCenter.DTOs.DoctorFindingsDTOs
{
    public class CreateFindingDto
    {
        public int AppointmentId { get; set; }
        public string Observations { get; set; }
        public string Recommendations { get; set; }
        public DateTime? NextSessionDate { get; set; }
    }
}
