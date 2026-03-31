namespace TherapyCenter.DTOs.TherapyDTOs
{
    public class CreateTherapyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
    }
}
