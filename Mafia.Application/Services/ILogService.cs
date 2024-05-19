using Mafia.Domain.Entities;
using System.Threading.Tasks;

namespace Mafia.WebApi.Services
{
    public interface ILogService
    {
        Task CreateLog(string controllerForLog, string actions, int? patientId = 0, dynamic oldVal = null, dynamic newVal = null);
    }
}
