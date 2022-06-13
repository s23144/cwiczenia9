using cwiczenia8.Models.DTO;
using System.Threading.Tasks;

namespace cwiczenia8.Services
{
    public interface IAccountDbService
    {
        Task RegisterAsync(AccountDTO accountDTO);
        Task <TokenDTO> LoginAsync(AccountDTO accountDTO);
        Task<TokenDTO> UpdateToken(string refreshToken);


    }
}
