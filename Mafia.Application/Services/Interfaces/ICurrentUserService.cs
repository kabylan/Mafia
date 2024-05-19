using Mafia.Domain.Entities;

namespace Mafia.Application.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string ApplicationUserId { get; set; }
        ApplicationUser ApplicationUser { get; set; }
    }
}