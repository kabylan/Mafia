using Mafia.Domain.AutoAudit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Entities
{
    /// <summary>
    /// Сущность Долножность и специальность и фио
    /// </summary>
    public class Position : ISoftDelete, IAuditedEntityBase
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string NameKg { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronimyc { get; set; }
        public string Pin { get; set; }
        public Organisation Organisation { get; set; }
        public int? OrganisationId { get; set; }
        public string GetFIOandPosition(int index =0 )
        {
            if (index == 0)
            {
                return LastName + " " + FirstName + " " + Patronimyc + " / " + NameRu;
            }
            else
            {
                return LastName + " " + FirstName + " " + Patronimyc + "\n / " + NameRu;
            }
        }
    }
}
