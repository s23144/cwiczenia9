using cwiczenia8.Models.DTO;
using cwiczenia8.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace cwiczenia8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorDbService _dbservice;

        public DoctorController (IDoctorDbService doctorDbService)
        {
            _dbservice = doctorDbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorsAsync()
        {
            var doctors = await _dbservice.GetDoctorsAsync();
            return Ok(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> PostDoctorsAsync([FromForm] DoctorDTO doctorDTO)
        {
            
            await _dbservice.AddDoctorAsync(doctorDTO);
            return Ok($"Doktor {doctorDTO.FirstName } {doctorDTO.LastName} dodany do bazy danych");
        }
        [HttpPut]
        [Route("{idDoctor}")]
        public async Task <IActionResult> ModifyDoctorAsync(int idDoctor,[FromForm]DoctorDTO doctorDTO)
        {
            try
            {
               await _dbservice.UpdateDoctorAsync(idDoctor, doctorDTO);
                return Ok($"Zmodyfikowano doktora o id {idDoctor}");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{idDoctor}")]
        public async Task <IActionResult> DeleteDoctorAsync(int idDoctor)
        {
            try
            {
                await _dbservice.DeleteDoctorAsync(idDoctor);
                return Ok($"Doktor o id {idDoctor} został usunięty z bazy danych");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
       
    }
}
