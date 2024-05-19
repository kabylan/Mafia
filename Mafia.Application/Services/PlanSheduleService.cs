using Mafia.Application.Utils;
using Mafia.Domain.AutoAudit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using Mafia.Domain.Data.Adapters;

namespace Mafia.WebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    //public class PlanSheduleService : IPlanSheduleService, IJob
    //{
    //    private readonly IServiceProvider _provider;
    //    //--->https://stackoverflow.com/questions/36332239/use-dbcontext-in-asp-net-singleton-injected-class
    //    private MafiaDbContext _context;
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    private readonly IAuthorizationService _authorizationService;

    //    public PlanSheduleService(MafiaDbContext context, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
    //    {
    //        _context = context;
    //        _httpContextAccessor = httpContextAccessor;
    //        _authorizationService = authorizationService;
    //    }


    //    //Shedule Create Record For Plan Reestr
    //    public async Task Execute(IJobExecutionContext context)
    //    {
           
    //    }

    //    public Task CreateRecordsForPlan()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
