namespace TherapyCenter2.DTOs.Doctor
{
    public class DoctorListDto
    {
        public int DoctorId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public string Specialization { get; set; }
        public string AvailableDays { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
