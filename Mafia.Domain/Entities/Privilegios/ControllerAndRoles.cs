using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Entities.Privilegios
{
    public class ControllerAndRoles
    {
        public int Id { get; set; }
        public ActionForUser ActionForUser { get; set; }
        public int ActionForUserId { get; set; }
        public IdentityRole IdentityRole { get; set; }
        public string IdentityRoleId { get; set; }
    }
}
