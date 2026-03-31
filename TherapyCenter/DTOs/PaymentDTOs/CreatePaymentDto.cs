namespace TherapyCenter.DTOs.PaymentDTOs
{
    public class CreatePaymentDto
    {
        public int AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
