using Mafia.Domain.Dto;
using Mafia.Domain.Dto.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mafia.Application.Services.AccountAndUser
{
    public interface IAccountService
    {
        Task<AuthorizationResponse> Token(Authorize authorize);

        Task<AuthorizationResponse> GetPrivilegios(string userName);

        Task<bool> CheckUserAsync(UserInfoForAuth user);

        Task<ClaimsIdentity> GetIdentityAsync(string login, string password);
    }
}