namespace TherapyCenter.DTOs.SlotDTOs
{
    public class CreateSlotDto
    {
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
