using Mafia.Domain.AutoAudit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Entities.Base
{
    /// <summary>
    /// Базовый класс GUID
    /// </summary>
    public class BaseEntityGuid : ISoftDelete, IAuditedEntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
