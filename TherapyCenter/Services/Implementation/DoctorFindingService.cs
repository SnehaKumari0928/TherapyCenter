using TherapyCenter.DTOs.DoctorFindingsDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class DoctorFindingService : IDoctorFindingService
    {
        private readonly IDoctorFindingRepository _repo;
        private readonly IAppointmentRepository _appointmentRepo;

        public DoctorFindingService(
            IDoctorFindingRepository repo,
            IAppointmentRepository appointmentRepo)
        {
            _repo = repo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task AddFinding(CreateFindingDto dto)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(dto.AppointmentId);

            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found");

            var finding = new DoctorFinding
            {
                AppointmentId = dto.AppointmentId,
                Observations = dto.Observations,
                Recommendations = dto.Recommendations,
                NextSessionDate = dto.NextSessionDate
            };

            await _repo.AddAsync(finding);
        }

        public async Task<List<DoctorFindingDto>> GetByAppointment(int appointmentId)
        {
            var findings = await _repo.GetByAppointmentId(appointmentId);

            return findings.Select(f => new DoctorFindingDto
            {
                Id = f.FindingId,
                Observations = f.Observations,
                Recommendations = f.Recommendations,
                NextSessionDate = f.NextSessionDate
            }).ToList();
        }

        public async Task<List<DoctorFindingDto>> GetByPatient(int patientId)
        {
            var findings = await _repo.GetByPatientIdAsync(patientId);

            return findings.Select(f => new DoctorFindingDto
            {
                Id = f.FindingId,
                Observations = f.Observations,
                Recommendations = f.Recommendations,
                NextSessionDate = f.NextSessionDate
            }).ToList();
        }
    }
}
