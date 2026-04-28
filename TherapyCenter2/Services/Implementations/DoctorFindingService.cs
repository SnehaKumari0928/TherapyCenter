using TherapyCenter2.DTOs.DoctorFinding;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Services.Implementations
{
    public class DoctorFindingService: IDoctorFindingService
    {

        private readonly IDoctorFindingRepository _repository;

        public DoctorFindingService(IDoctorFindingRepository repository)
        {
            _repository = repository;
        }

        public async Task<DoctorFindingResponseDto> CreateAsync(CreateDoctorFindingDto dto)
        {
            var finding = new DoctorFinding
            {
                AppointmentId = dto.AppointmentId,
                Observations = dto.Observations,
                Recommendations = dto.Recommendations,
                NextSessionDate = dto.NextSessionDate,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.AddAsync(finding);

            return MapDoctorFindingResponse(created);
        }

        public async Task<List<DoctorFindingResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(MapDoctorFindingResponse).ToList();
        }

        public async Task<DoctorFindingResponseDto> GetByIdAsync(int id)
        {
            var finding = await _repository.GetByIdAsync(id);

            if (finding == null)
                throw new Exception("Finding not found");

            return MapDoctorFindingResponse(finding);
        }

        public async Task<List<DoctorFindingResponseDto>> GetByAppointmentAsync(int appointmentId)
        {
            var list = await _repository.GetByAppointmentIdAsync(appointmentId);
            return list.Select(MapDoctorFindingResponse).ToList();
        }

        public async Task<DoctorFindingResponseDto> UpdateAsync(int id, UpdateDoctorFindingDto dto)
        {
            var finding = await _repository.GetByIdAsync(id);

            if (finding == null)
                throw new Exception("Finding not found");

            finding.Observations = dto.Observations;
            finding.Recommendations = dto.Recommendations;
            finding.NextSessionDate = dto.NextSessionDate;

            await _repository.UpdateAsync(finding);

            return MapDoctorFindingResponse(finding);
        }

        public async Task DeleteAsync(int id)
        {
            var finding = await _repository.GetByIdAsync(id);

            if (finding == null)
                throw new Exception("Finding not found");

            await _repository.DeleteAsync(finding);
        }

        public async Task<List<DoctorFindingResponseDto>> GetByPatientIdAsync(int patientId)
        {
            var list = await _repository.GetByPatientIdAsync(patientId);

            return list.Select(MapDoctorFindingResponse).ToList();
        }

        private static DoctorFindingResponseDto MapDoctorFindingResponse(DoctorFinding f)
        {
            return new DoctorFindingResponseDto
            {
                FindingId = f.FindingId,
                AppointmentId = f.AppointmentId,
                Observations = f.Observations,
                Recommendations = f.Recommendations,
                NextSessionDate = f.NextSessionDate,
                CreatedAt = f.CreatedAt
            };
        }
    }
}
