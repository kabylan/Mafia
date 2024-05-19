using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Entities.Privilegios
{
    public class ActionCategoryForUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameRu { get; set; }
        public int OrderByField { get; set; }
        public List<ActionForUser> ActionForUsers { get; set; }
    }
}
