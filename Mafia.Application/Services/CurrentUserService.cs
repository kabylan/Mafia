using Mafia.Application.Services.Interfaces;
using Mafia.Domain.Data.Adapters;
using Mafia.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace Mafia.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly MafiaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(MafiaDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            if (_httpContextAccessor.HttpContext.User.Identity.Name is not null)
            {
                string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                ApplicationUserId = userName;

                ApplicationUser = _context.ApplicationUsers
                    .FirstOrDefault(e => e.UserName == userName);
            }
        }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}