using cwiczenia8.Models;
using cwiczenia8.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia8.Services
{
    public class DoctorDbService : IDoctorDbService
    {
        private readonly MainDbContext _dbContex;

        public DoctorDbService(MainDbContext mainDbContext)
        {
            _dbContex = mainDbContext;
        }

        public async Task AddDoctorAsync(DoctorDTO dto)
        {
            var doctor = new Doctor
            {
                FirsName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            await _dbContex.Doctors.AddAsync(doctor);
            await _dbContex.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int idDoctor)
        {
           await CheckIfDoctorExistsAsync(idDoctor);

            var doctor = new Doctor { IdDoctor = idDoctor };
             _dbContex.Attach(doctor);
            _dbContex.Remove(doctor);

            await _dbContex.SaveChangesAsync();



        }

        public async Task<IEnumerable<DoctorDTO>> GetDoctorsAsync()
        {
            return await _dbContex.Doctors
                .Select(e => new DoctorDTO
                {
                   
                   FirstName=e.FirsName,
                   LastName=e.LastName,
                   Email=e.Email


                }).ToListAsync();
        }

        public async Task UpdateDoctorAsync(int idDoctor, DoctorDTO dto)
        {
            await CheckIfDoctorExistsAsync(idDoctor);
            
            var doctor = await _dbContex.Doctors.FirstOrDefaultAsync(e=> e.IdDoctor == idDoctor);
            doctor.FirsName = dto.FirstName;
            doctor.LastName = dto.LastName;
            doctor.Email = dto.Email;

            await _dbContex.SaveChangesAsync();



        }


       
        private async Task CheckIfDoctorExistsAsync(int idDoctor)
        {
           var check =  await _dbContex.Doctors.AnyAsync(e => e.IdDoctor == idDoctor);
            if (!check)
            {
                throw new Exception($"Doktor o id {idDoctor} nie istnieje w bazie danych");
            }
        
        
        }
            
             
    }
}
