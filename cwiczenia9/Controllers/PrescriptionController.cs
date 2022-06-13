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
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionDbService _dbService; 

        public PrescriptionController (IPrescriptionDbService prescriptionDbService)
        {
            _dbService = prescriptionDbService;
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetPresAsync(int id)
        {
            try
            {
                var prescription = await _dbService.GetPrescriptionAsync(id);
                return Ok(prescription);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
