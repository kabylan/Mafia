﻿using System.Collections.Generic;
using Mafia.Domain.Entities;

namespace Mafia.Domain.AutoAudit
{
    public class UserSession : IUserSession
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TenantId { get; set; } = -1;
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public List<KeyValuePair<string, string>> ExposedClaims { get; set; }
        public bool DisableTenantFilter { get; set; } = false;


        public UserSession()
        {
        }

        public UserSession(ApplicationUser user)
        {
            UserId = user.Id;
            UserName = user.UserName;
        }
    }
}
