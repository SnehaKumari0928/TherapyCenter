namespace TherapyCenter.DTOs.DoctorFindingsDTOs
{
    public class DoctorFindingDto
    {
        public int Id { get; set; }
        public string Observations { get; set; }
        public string Recommendations { get; set; }
        public DateTime? NextSessionDate { get; set; }
    }
}
