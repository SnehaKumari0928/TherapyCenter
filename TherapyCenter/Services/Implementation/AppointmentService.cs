using TherapyCenter.DTOs.AppointmentDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class AppointmentService: IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly ISlotRepository _slotRepo;

        public AppointmentService(IAppointmentRepository repo,ISlotRepository slotRepo)
        {
            _appointmentRepo = repo;
            _slotRepo = slotRepo;
        }

        public async Task BookAppointment(BookAppointmentDto dto)
        {

            var slot = await _slotRepo.GetByIdAsync(dto.SlotId);

            if (slot == null)
            {
                throw new KeyNotFoundException("Slot not found");
            }

            if (slot.IsBooked)
            {
                throw new ArgumentException("Slot already booked");

            }

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                TherapyId = dto.TherapyId,
                AppointmentDate = slot.Date,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime,
                Status = "Scheduled"
            };

            await _appointmentRepo.AddAsync(appointment);
            slot.IsBooked = true;

            await _slotRepo.UpdateSlotAsync(slot);


        }
        public async Task CancelAppointment(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);

            if(appointment == null)
                throw new KeyNotFoundException("Appointment not found");

            appointment.Status = "Cancelled";

            await _appointmentRepo.UpdateAsync(appointment);
        }
    }
}
