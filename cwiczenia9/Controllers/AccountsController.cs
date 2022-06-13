using cwiczenia8.Models;
using cwiczenia8.Models.DTO;
using cwiczenia8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cwiczenia8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountDbService _dbService;

        public AccountsController( IAccountDbService accountDbService)
        {
            _dbService = accountDbService;
        }


        [HttpPost("register")]

        public async Task <IActionResult> RegisterUserAsync([FromForm]AccountDTO accountDTO)
        {
            try
            {
                await _dbService.RegisterAsync(accountDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task <IActionResult> LoginUserAsync ([FromForm] AccountDTO accountDTO)
        {

            try
            {
             var dto =  await _dbService.LoginAsync(accountDTO);
                return Ok(dto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("updateAccessToken")]

        public async Task <IActionResult> UpdateAccessTokenAsync(string refreshToken)
        {
            try
            {
                var res = await _dbService.UpdateToken(refreshToken);
                return Ok(res);
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

      
    }
}
