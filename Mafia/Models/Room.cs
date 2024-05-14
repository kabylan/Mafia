using Mafia.Enums;

namespace Mafia.Models
{
    public class Room
    {
        public int Id { get; set; }
        
        // Админ комнаты
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        // пароль для подключения к игре
        public int Password { get; set; }
        
        // название комнаты
        public string Name { get; set; }

        // статус комнаты - ожидание, идет игра, завершена
        public RoomStatusEnum Status { get; set; }

        // время создания комнаты
        public DateTime CreatedAt { get; set; }

        // время завершения игры и закрытия комнаты
        public DateTime EndedAt { get; set; }

        // игроки в комнате
        public List<Player> Players { get; set; }
    }
}
