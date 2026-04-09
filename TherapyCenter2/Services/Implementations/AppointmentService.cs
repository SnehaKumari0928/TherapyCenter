using TherapyCenter2.DTOs.Appointment;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Services.Implementations
{
    public class AppointmentService: IAppointmentService
    {

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ISlotRepository _slotRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  ISlotRepository slotRepository)
        {
            _appointmentRepository = appointmentRepository;
            _slotRepository = slotRepository;
        }

        public async Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto)
        {
            var slot = await _slotRepository.GetByIdAsync(dto.SlotId);

            if (slot == null)
                throw new Exception("Slot not found");

            if (slot.IsBooked)
                throw new Exception("Slot already booked");

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                TherapyId = dto.TherapyId,
                ReceptionistId = dto.ReceptionistId,
                AppointmentDate = slot.Date,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime,
                Status = "Scheduled",
                Notes = dto.Notes
            };

            var created = await _appointmentRepository.AddAsync(appointment);

            slot.IsBooked = true;
            await _slotRepository.UpdateAsync(slot);

            return MapAppointmentResponse(created);
        }

        public async Task<List<AppointmentResponseDto>> GetAllAsync()
        {
            var list = await _appointmentRepository.GetAllAsync();
            return list.Select(MapAppointmentResponse).ToList();
        }

        public async Task<AppointmentResponseDto> GetByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found");

            return MapAppointmentResponse(appointment);
        }

        public async Task<AppointmentResponseDto> UpdateAsync(int id, AppointmentUpdateDto dto)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found");

            appointment.Notes = dto.Notes;

            await _appointmentRepository.UpdateAsync(appointment);

            return MapAppointmentResponse(appointment);
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found");

            await _appointmentRepository.DeleteAsync(appointment);
        }

        public async Task CompleteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found");

            appointment.Status = "Completed";

            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task CancelAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                throw new Exception("Appointment not found");

            appointment.Status = "Cancelled";

            await _appointmentRepository.UpdateAsync(appointment);
        }

        private static AppointmentResponseDto MapAppointmentResponse(Appointment a)
        {
            return new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                TherapyId = a.TherapyId,
                AppointmentDate = a.AppointmentDate,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                Status = a.Status,
                Notes = a.Notes
            };
        }
    }
}
