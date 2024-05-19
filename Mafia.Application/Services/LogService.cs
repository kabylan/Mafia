using Mafia.Application;
using Mafia.Domain.AutoAudit;
using Mafia.Domain.Data.Adapters;
using Mafia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mafia.WebApi.Services
{
    public class LogService : ILogService
    {
        private readonly IServiceProvider _provider;
        //--->https://stackoverflow.com/questions/36332239/use-dbcontext-in-asp-net-singleton-injected-class
        private MafiaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LogService(UserManager<ApplicationUser> userManager, MafiaDbContext context, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async Task CreateLog(string controllerForLog, string actions, int? patientId = 0, dynamic oldVal = null, dynamic newVal = null)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

                var log = new Log()
                {
                    Action = actions,
                    Controller = controllerForLog,
                    PatientId = patientId,
                    PatientFIO = user.FIO,
                    OldValue = oldVal != null ? JsonSerializer.Serialize(oldVal) : "",
                    NewValue = newVal != null ? JsonSerializer.Serialize(newVal) : "",
                    FIOUserName = user != null ? user.FIO : "",
                    UserName = _httpContextAccessor.HttpContext.User.Identity.Name,
                    Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Created = DateTime.Now,
                };
                _context.Logs.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}
