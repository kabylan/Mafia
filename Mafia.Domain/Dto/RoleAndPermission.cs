using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Dto
{
    public class RoleAndPermissions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Permissions> Permissions { get; set; } = new List<Permissions>();
    }

    public class Permissions
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
