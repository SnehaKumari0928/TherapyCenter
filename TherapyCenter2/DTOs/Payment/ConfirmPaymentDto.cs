namespace TherapyCenter2.DTOs.Payment
{
    public class ConfirmPaymentDto
    {
        public string PaymentIntentId { get; set; } = string.Empty;
        public int AppointmentId {  get; set; }
        public string PaymentStatus {  get; set; }
    }
}
