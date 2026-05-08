using System.ComponentModel.DataAnnotations;

namespace TherapyCenter2.DTOs.Slot
{
    public class CreateBulkSlotDto
    {

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public int DurationMinutes { get; set; }
    }
}
