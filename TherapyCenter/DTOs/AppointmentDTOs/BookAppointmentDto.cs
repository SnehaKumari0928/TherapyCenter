namespace TherapyCenter.DTOs.AppointmentDTOs
{
    public class BookAppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int TherapyId { get; set; }
        public int SlotId { get; set; }

        public int? ReceptionistId { get; set; }
    }
}
