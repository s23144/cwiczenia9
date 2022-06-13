using cwiczenia8.Models.DTO.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cwiczenia8.Services
{
    public interface IPrescriptionDbService
    {
        Task<IEnumerable<PrescritptionDTO>> GetPrescriptionAsync(int id);
    }
}
