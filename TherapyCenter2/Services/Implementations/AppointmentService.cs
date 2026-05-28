using System.Security.Claims;
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
        private readonly IPatientRepository _patientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITherapyRepository _therapyRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  ISlotRepository slotRepository, IPatientRepository patientRepository,
            IHttpContextAccessor httpContextAccessor,
            ITherapyRepository therapyRepository, IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _slotRepository = slotRepository;
             _patientRepository = patientRepository;
            _httpContextAccessor = httpContextAccessor;
            _therapyRepository = therapyRepository; 
            _doctorRepository = doctorRepository;
        }

        public async Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto)
        {
            var slot = await _slotRepository.GetByIdAsync(dto.SlotId);

            if (slot == null)
                throw new Exception("Slot not found");

            if (slot.IsBooked)
                throw new Exception("Slot already booked");

            var userIdClaim = _httpContextAccessor.HttpContext?
               .User
               .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("Invalid token");

            int userId = int.Parse(userIdClaim);
            var patient = await _patientRepository.GetByUserIdAsync(userId);
            if (patient == null)
                throw new Exception("Patient not found");

            var appointment = new Appointment
            {
                PatientId = patient.PatientId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DoctorId = dto.DoctorId,
                TherapyId = dto.TherapyId,
                ReceptionistId = dto.ReceptionistId == 0 ? null : dto.ReceptionistId,
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


            foreach (var a in list)
            {
                Console.WriteLine(
                    $"Id: {a.AppointmentId}, ReceptionistId: {a.ReceptionistId}"
                );
            }
            return list.Select(MapAppointmentResponse).ToList();
        }

        public async Task<List<AppointmentResponseDto>> GetByPatientIdAsync()
        {

            var userIdClaim = _httpContextAccessor.HttpContext?
               .User
               .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("Invalid token");

            int userId = int.Parse(userIdClaim);
            var patient = await _patientRepository.GetByUserIdAsync(userId);
            if (patient == null)
                throw new Exception("Patient not found");

            var list = await _appointmentRepository.GetByPatientIdAsync(patient.PatientId);
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
                ReceptionistId = a.ReceptionistId,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                Status = a.Status,
                Notes = a.Notes
            };
        }

        public async Task<List<AppointmentResponseDto>> GetByDoctorIdAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?
               .User
               .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("Invalid token");

            int userId = int.Parse(userIdClaim);
            var doctor = await _doctorRepository.GetByUserIdAsync(userId);
            if (doctor == null)
                throw new Exception("Doctor not found");
            var list = await _appointmentRepository.GetByDoctorIdAsync(doctor.DoctorId);

            return list.Select(a => new
             AppointmentResponseDto
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

            }
            ).ToList();
        }

        public async Task<AppointmentResponseDto> CreateWalkInAsync(WalkInAppointmentDto dto)
        {
            var slot = await _slotRepository.GetByIdAsync(dto.SlotId);

            if (slot == null)
                throw new Exception("Slot not found");

            Console.WriteLine($"Slot Id: {slot.SlotId}");
            Console.WriteLine($"Date: {slot.Date}");
            Console.WriteLine($"Start: {slot.StartTime}");
            Console.WriteLine($"End: {slot.EndTime}");

            if (slot.IsBooked)
                throw new Exception("Slot already booked");

            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CreatedAt = DateTime.Now
            };

            await _patientRepository.AddAsync(patient);

            var userIdClaim = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("Invalid token");

            int receptionistId = int.Parse(userIdClaim);

            var appointment = new Appointment
            {
                PatientId = patient.PatientId,
                DoctorId = dto.DoctorId,
                TherapyId = dto.TherapyId,
                ReceptionistId = receptionistId,

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

        public async Task<List<AppointmentResponseDto>> GetAppointmentsForGuardianAsync()
        {

            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null)
                throw new Exception("User not authenticated");

            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new Exception("UserId not found in token");

            var guardianId = int.Parse(claim.Value);




            var patients = await _patientRepository.GetByGuardianIdAsync(guardianId);

            if (patients == null || !patients.Any())
                return new List<AppointmentResponseDto>();

            var patientIds = patients.Select(p => p.PatientId).ToList();


            var appointments = await _appointmentRepository.GetAllAsync();

            var result = appointments
                .Where(a => patientIds.Contains(a.PatientId))
                .Select(MapAppointmentResponse)
                .ToList();

            return result;
        }
    }
}
