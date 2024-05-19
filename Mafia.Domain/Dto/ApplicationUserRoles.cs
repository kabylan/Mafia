using Mafia.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Dto
{
    public class ApplicationUserRoles : IdentityUser
    {
        public string Pin { get; set; }
        public Organisation Organisation { get; set; }
        public int? OrganisationId { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public bool Block { get; set; }
        public string IdentityRoles { get; set; }
        public string IdentityRoleId { get; set; }
    }
}
