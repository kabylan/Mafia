using Mafia.Domain.AutoAudit;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mafia.Domain.Entities.Base
{
    /// <summary>Базовый класс для аудита</summary>
    public abstract class AuditableEntity : ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Создано пользователем ID</summary>
        [Display(Name = "Создано пользователем ID")]
        public string CreatedBy { get; set; }

        /// <summary>Дата создания записи</summary>
        [Display(Name = "Дата создания записи")]
        public DateTime Created { get; set; }

        /// <summary>Обновлено пользователем ID</summary>
        [Display(Name = "Обновлено пользователем ID")]
        public string LastModifiedBy { get; set; }

        /// <summary>Дата последнего обновления записи</summary>
        [Display(Name = "Дата последнего обновления записи")]
        public DateTime? LastModified { get; set; }
    }
}