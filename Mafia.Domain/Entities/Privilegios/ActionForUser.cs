using Newtonsoft.Json;

namespace Mafia.Domain.Entities.Privilegios
{
    public class ActionForUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameRu { get; set; }
        [JsonIgnore]
        public ActionCategoryForUser ActionCategoryForUser { get; set; }
        public int ActionCategoryForUserId { get; set; }
    }
}
