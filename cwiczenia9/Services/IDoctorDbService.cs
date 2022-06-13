using cwiczenia8.Models.DTO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cwiczenia8.Services
{
    public interface IDoctorDbService
    {
        Task<IEnumerable<DoctorDTO>> GetDoctorsAsync();
        Task AddDoctorAsync(DoctorDTO dto);
        Task UpdateDoctorAsync(int idDoctor,DoctorDTO dto);
        Task DeleteDoctorAsync(int idDoctor);
    }
}
