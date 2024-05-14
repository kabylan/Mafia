using Mafia.Enums;

namespace Mafia.Models
{
    // Админ, который создает игру
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomId {  get; set; }
        public Room Room { get; set; }
    }
}
