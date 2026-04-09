using System.ComponentModel.DataAnnotations;

namespace TherapyCenter2.DTOs.Slot
{
    public class UpdateSlotDto
    {

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
