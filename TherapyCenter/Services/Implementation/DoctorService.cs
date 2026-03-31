using TherapyCenter.DTOs.DoctorDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IUserRepository _userRepo;

        public DoctorService(IDoctorRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        public async Task<List<DoctorDto>> GetAllDoctors()
        {
            var doctors = await _repo.GetAllAsync();

            return doctors.Select(d => new DoctorDto
            {
                Id = d.DoctorId,
                Name = $"{d.User.FirstName} {d.User.LastName}",
                Specialization = d.Specialization
            }).ToList();
        }

        public async Task<DoctorDto> GetDoctorById(int id)
        {
            var doctor = await _repo.GetByIdAsync(id);

            if (doctor == null)
                throw new KeyNotFoundException("Doctor not found");

            return new DoctorDto
            {
                Id = doctor.DoctorId,
                Name = $"{doctor.User.FirstName} {doctor.User.LastName}",
                Specialization = doctor.Specialization
            };
        }

        public async Task AddDoctor(CreateDoctorDto dto)
        {
            var user = await _userRepo.GetByIdAsync(dto.UserId);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (user.Role != "Doctor")
                throw new ArgumentException("User must have Doctor role");

            var doctor = new Doctor
            {
                UserId = dto.UserId,
                Specialization = dto.Specialization,
                Bio = dto.Bio
            };

            await _repo.AddAsync(doctor);
        }

        
        
    }
}
