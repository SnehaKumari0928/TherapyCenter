namespace TherapyCenter.Entities
{
    public class Therapy
    {
        public int TherapyId {  get; set; }
        public string Name {  get; set; }
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public Decimal Cost {  get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        
    }
}
