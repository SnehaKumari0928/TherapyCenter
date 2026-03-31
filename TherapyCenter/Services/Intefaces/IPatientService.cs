using TherapyCenter.DTOs.PatientDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface IPatientService
    {

        Task<List<PatientDto>> GetAllPatients();
        Task AddPatient(CreatePatientDto dto);
    }
}
