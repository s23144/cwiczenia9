using cwiczenia8.Models;
using cwiczenia8.Models.DTO;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cwiczenia8.Services
{
    public class AccountDbService : IAccountDbService
    {

        private readonly MainDbContext _mainDbContext;
        private readonly IConfiguration _conf;

        public AccountDbService (MainDbContext mainDb, IConfiguration configuration)
        {
            _mainDbContext = mainDb;    
            _conf = configuration;
        }

        public async Task RegisterAsync(AccountDTO accountDTO)
        {
            var userExists = await _mainDbContext.Accounts.AnyAsync(e => e.Login == accountDTO.Login);

            if (userExists)
            {
                throw new Exception("Taki użytkowink już istnieje");
            }

            var hashedPassword = GenereteHashedAndSaltedPassword(accountDTO.Password);

            var account = new Account
            {
                Login = accountDTO.Login,
                Password = hashedPassword.Item1,
                Salt = hashedPassword.Item2,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddHours(12)

            };
            await _mainDbContext.Accounts.AddAsync(account);
            await _mainDbContext.SaveChangesAsync();



        }
        public async Task<TokenDTO> LoginAsync (AccountDTO accountDTO)
        {
            var userLoggingIn = await _mainDbContext.Accounts.FirstOrDefaultAsync(e => e.Login == accountDTO.Login);

            if(userLoggingIn == null)
            {
                throw new Exception("Nie znaleziono użytkownika");

            }
            if(userLoggingIn.Password != GetHashedSaltedPassword(accountDTO.Password, userLoggingIn.Salt))
                {
                throw new Exception("Hasło nie prawidłowe");
            }

            var opt = GetJwtSecurityToken();

            userLoggingIn.RefreshToken = GenerateRefreshToken();
            userLoggingIn.RefreshTokenExp = DateTime.Now.AddHours(12);

            await _mainDbContext.SaveChangesAsync();

            return new TokenDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(opt).ToString(),
                RefreshToken = userLoggingIn.RefreshToken
            };

            
        }

        public async Task<TokenDTO> UpdateToken(string refreshToken)
        {
            var user = await _mainDbContext.Accounts.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);

            if(user == null)
            {
                throw new Exception("Nieznaleziono użytkownika");
            }

            if(user.RefreshTokenExp < DateTime.Now)
            {
                throw new Exception("RefreshToken wygasł");
            }

            var token = GetJwtSecurityToken();

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExp = DateTime.Now.AddHours(12);

            await _mainDbContext.SaveChangesAsync();

            return new TokenDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token).ToString(),
                RefreshToken = user.RefreshToken
            };
        }

        public JwtSecurityToken GetJwtSecurityToken()
        {

            var claims = new Claim[]
            {
                new(ClaimTypes.Role, "user")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Secret"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("https://localhost:5001", "https://localhost:5001", claims, expires: DateTime.UtcNow.AddMinutes(5), signingCredentials: creds);

             
            return token;
        }
        public string GenerateRefreshToken()
        {

            var refreshToken = "";

            using (var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }

            return refreshToken;    
        }

        public Tuple<string, string> GenereteHashedAndSaltedPassword(string pass)
        {
            byte[] saltBytes = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(saltBytes);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(

                password: pass,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8

                ));

            string salt = Convert.ToBase64String(saltBytes);

            return new(hashed, salt);
        }

        public string GetHashedSaltedPassword(string pass, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            
            string hashedSalted = Convert.ToBase64String(KeyDerivation.Pbkdf2(

                password: pass,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8

                ));
            return hashedSalted;

        }
       

    }
}
