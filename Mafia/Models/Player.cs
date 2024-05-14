using Mafia.Enums;

namespace Mafia.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }


        // Роль игрока - мафия, мирный житель, доктор и тд.
        public PlayerRoleEnum PlayerGameRole { get; set; }

        // комната в которую подключился игрок
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
