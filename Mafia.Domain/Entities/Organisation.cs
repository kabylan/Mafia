using System.ComponentModel.DataAnnotations.Schema;
using Mafia.Domain.AutoAudit;

namespace Mafia.Domain.Entities
{
    /// <summary>
    /// Сущность организации
    /// </summary>
    public class Organisation : ISoftDelete, IAuditedEntityBase
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string NameKg { get; set; }
        public string NameEn { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public double? Longitude { get; set; }
        public double? Lattitude { get; set; }
        public string Phone { get; set; }
        public bool ShowOnMap { get; set; }

    }
}
