using TherapyCenter.DTOs.PatientDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class PatientService: IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<PatientDto>> GetAllPatients()
        {
            var patients = await _repo.GetAllAsync();

            return patients.Select(p => new PatientDto
            {
                Id = p.PatientId,
                Name = $"{p.FirstName} {p.LastName}",
                Gender = p.Gender
            }).ToList();
        }
        public async Task AddPatient(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                GuardianId = dto.GuardianId,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,


            };
            await _repo.AddAsync(patient);
        }
    }
}
