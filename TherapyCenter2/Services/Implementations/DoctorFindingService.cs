using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using TherapyCenter2.DTOs.DoctorFinding;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Services.Implementations
{
    public class DoctorFindingService: IDoctorFindingService
    {

        private readonly IDoctorFindingRepository _repository;
        private readonly IPatientRepository _patientRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoctorFindingService(IDoctorFindingRepository repository, IPatientRepository patientRepository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _patientRepository = patientRepository;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<DoctorFindingResponseDto> GetByAppointmentAsync(int appointmentId)
        {
            var list = await _repository.GetByAppointmentIdAsync(appointmentId);
            if (list == null) throw new Exception("Not found");
            return MapDoctorFindingResponse(list);
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

        public async Task<List<DoctorFindingResponseDto>> GetByPatientIdAsync()
        {

            var userIdClaim =
      _httpContextAccessor.HttpContext?
      .User
      .FindFirst(
          ClaimTypes.NameIdentifier
      )?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("Invalid token");

            int userId = int.Parse(userIdClaim);

            var patient =
                await _patientRepository
                .GetByUserIdAsync(userId);

            if (patient == null)
                throw new Exception(
                    "Patient not found");
        
                    var list = await _repository.GetByPatientIdAsync(patient.PatientId);

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
