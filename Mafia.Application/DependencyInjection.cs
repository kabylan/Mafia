using Mafia.Application.Mappings;
using Mafia.Application.Services;
using Mafia.Application.Services.AccountAndUser;
using Mafia.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Mafia.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            Type type = typeof(AutoMapperProfile);
            Assembly assembly = type.Assembly;
            services.AddAutoMapper(assembly);

            return services;
        }
    }
}