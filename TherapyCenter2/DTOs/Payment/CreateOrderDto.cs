namespace TherapyCenter2.DTOs.Payment
{
    public class CreateOrderDto
    {

        public int AppointmentId { get; set; }
        public decimal Amount { get; set; }
    }
}
