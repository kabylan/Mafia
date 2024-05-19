using Mafia.Domain.AutoAudit.AuditEntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mafia.Domain.Entities
{
    public class AuditRecord : IAudit
    {
        /// <summary>PK, Порядковый номер</summary>
        [Key]
        public int AuditId { get; set; }

        /// <summary>Дата изменения объекта</summary>
        public DateTime AuditDate { get; set; }

        /// <summary>Действие пользователя</summary>
        public string AuditAction { get; set; }

        /// <summary>Наименование компьютера</summary>
        public string AuditMachineName { get; set; }

        /// <summary>Пользователь</summary>
        public string AuditUserName { get; set; }

        /// <summary>Пользователь</summary>
        public string AuditUserId { get; set; }

        /// <summary>Измененные поля в JSON</summary>
        public string JsonChangedData { get; set; }

        #region Запись

        /// <summary>PK записи</summary>
        public int RecordId { get; set; }

        /// <summary>Тип записи</summary>
        public AuditRecordType AuditRecordType { get; set; }

        #endregion Запись
    }
}