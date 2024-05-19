namespace Mafia.Domain.AutoAudit.AuditEntityFramework
{
    public class AuditChangedJsonData
    {
        public string ColumnNameRu { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
    }
}