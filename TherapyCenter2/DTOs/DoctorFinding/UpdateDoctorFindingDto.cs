namespace TherapyCenter2.DTOs.DoctorFinding
{
    public class UpdateDoctorFindingDto
    {

        public string? Observations { get; set; }

        public string? Recommendations { get; set; }

        public DateOnly? NextSessionDate { get; set; }
    }
}
