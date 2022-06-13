using cwiczenia8.Models;
using cwiczenia8.Models.DTO;
using cwiczenia8.Models.DTO.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia8.Services
{
    public class PrescriptionDbService : IPrescriptionDbService
    {
        private readonly MainDbContext _dbContext;

        public PrescriptionDbService (MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PrescritptionDTO>> GetPrescriptionAsync(int id)
        {
            var prescriptionExists = await _dbContext.Prescriptions.AnyAsync(e => e.IdPrescription == id);

            if (!prescriptionExists)
            {
                throw new Exception($"Recepta {id} nie istnieje w bazie danych");
            }

            return await _dbContext.Prescriptions
                .Include(e => e.PrescriptionsMedicaments)
                .Where(e => e.IdPrescription == id)
                
                
                .Select(e => new PrescritptionDTO
                {
                    DoctorFirstName = e.DoctorNavigation.FirsName,
                    DoctorLastName = e.DoctorNavigation.LastName,
                    DoctorEmail = e.DoctorNavigation.Email,
                    PatientFirstName = e.PatientNavigation.FirsName,
                    PatientLastName = e.PatientNavigation.LastName,
                    PatientBirthDate = e.PatientNavigation.BirthDate,
                    
                    Medicaments = e.PrescriptionsMedicaments.Select(e => new MedicamentDTO
                    {
                        Name = e.MedicamentNavigation.Name,
                        Description = e.MedicamentNavigation.Description,
                        Type = e.MedicamentNavigation.Type
                    } ).ToList()

                }).ToListAsync();


        }
    }
}
