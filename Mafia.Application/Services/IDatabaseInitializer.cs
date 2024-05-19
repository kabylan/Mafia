using System.Threading.Tasks;

namespace Mafia.Application.Services
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
