using System;
using System.Collections.Generic;

namespace Mafia.Domain.AutoAudit
{
    public interface IUserSession
    {
        string UserId { get; set; }
        int TenantId { get; set; }
        List<string> Roles { get; set; }
        string UserName { get; set; }
        bool DisableTenantFilter { get; set; }
    }
}
