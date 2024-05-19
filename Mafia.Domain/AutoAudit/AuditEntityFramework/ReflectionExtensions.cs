using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mafia.Domain.AutoAudit.AuditEntityFramework
{
    public static class ReflectionExtensions
    {
        public static string GetFieldDisplayValue(this MemberInfo propertyInfo)
        {
            var displayAttribute = propertyInfo.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            string name = propertyInfo.Name;
            if (displayAttribute != null)
                name = displayAttribute.Name;
            return name;
        }
    }
}