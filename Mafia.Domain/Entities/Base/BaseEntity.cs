using Mafia.Domain.AutoAudit;
using System.ComponentModel.DataAnnotations;

namespace Mafia.Domain.Entities.Base
{
    /// <summary>Общий базовый класс</summary>
    public abstract class BaseEntity : ISoftDelete, IAuditedEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}