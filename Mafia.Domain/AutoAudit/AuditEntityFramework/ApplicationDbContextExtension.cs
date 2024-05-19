namespace Mafia.Domain.AutoAudit.AuditEntityFramework
{
    public static class ApplicationDbContextExtension
    {
        //public static void AddAuditSettings()
        //{
        //    Configuration.Setup()
        //        .UseEntityFramework(ef => ef
        //        .AuditTypeExplicitMapper(m => m
        //        .Map<FreeWorkTimeUser, AuditRecord>((record, audit) =>
        //        {
        //            audit.AuditUserId = record.LastModifiedBy ?? record.CreatedBy;
        //            audit.RecordId = record.Id;
        //            audit.AuditRecordType = AuditRecordType.FreeWorkTimeUser;
        //        })
        //        .AuditEntityAction<IAudit>((evt, entry, auditEntity) =>
        //        {
        //            List<AuditChangedJsonData> changedJsonData = new();
        //            if (entry.Action.Equals("Update", StringComparison.OrdinalIgnoreCase))
        //            {
        //                foreach (var json in entry.Changes)
        //                {
        //                    MemberInfo memberInfo = entry.EntityType.GetProperty(json.ColumnName);
        //                    AuditChangedJsonData jsonData = new()
        //                    {
        //                        ColumnNameRu = memberInfo.GetFieldDisplayValue(),
        //                        ColumnName = json.ColumnName,
        //                        NewValue = json.NewValue?.ToString(),
        //                        OriginalValue = json.OriginalValue?.ToString()
        //                    };
        //                    changedJsonData.Add(jsonData);
        //                }
        //            }
        //            auditEntity.JsonChangedData = JsonConvert.SerializeObject(changedJsonData);
        //            auditEntity.AuditDate = DateTime.Now;
        //            auditEntity.AuditUserName = evt.Environment.UserName;
        //            auditEntity.AuditMachineName = evt.Environment.MachineName;
        //            auditEntity.AuditAction = entry.Action; // Insert, Update, Delete
        //        })));
        //}
    }
}