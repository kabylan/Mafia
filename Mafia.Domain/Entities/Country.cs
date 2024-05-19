using Mafia.Domain.AutoAudit;

namespace Mafia.Domain.Entities
{
    /// <summary>
    /// Сущность страна
    /// </summary>
    public class Country : ISoftDelete, IAuditedEntityBase
	{
		public int Id { get; set; }
		public string Alfa2 { get; set; }
		public string Alfa3 { get; set; }
		public string NameRu { get; set; }
		public string NameEn { get; set; }
		public int Code { get; set; }
 	}
}
