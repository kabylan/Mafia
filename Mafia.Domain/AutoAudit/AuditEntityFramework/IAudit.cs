using System;

namespace Mafia.Domain.AutoAudit.AuditEntityFramework
{
    public interface IAudit
    {
        int AuditId { get; set; }
        DateTime AuditDate { get; set; }
        string AuditAction { get; set; }
        string AuditUserName { get; set; }
        string AuditMachineName { get; set; }
        string AuditUserId { get; set; }
        string JsonChangedData { get; set; }
    }
}