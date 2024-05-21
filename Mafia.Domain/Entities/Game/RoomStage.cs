using System.ComponentModel.DataAnnotations;

namespace Mafia.Domain.Entities.Game
{
    public class RoomStage
    {
        [Key]
        public int Id { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public bool Day { get; set; } = false;
        public bool Nigth { get; set; } = false;
        public bool Mafia { get; set; } = false;
        public bool Doctor { get; set; } = false;
        public bool Putana { get; set; } = false;
        public bool Commisar_whore { get; set; } = false;
        public int Stage { get; set; } = 1;
    }
}
