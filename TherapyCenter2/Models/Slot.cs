namespace TherapyCenter2.Models
{
    public class Slot
    {
        public int SlotId { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public bool IsBooked { get; set; } = false;
    }
}
