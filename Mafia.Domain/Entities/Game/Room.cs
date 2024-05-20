using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mafia.Domain.Entities.Game
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status? Status { get; set; }

        public string RoomNumber { get; set; }
        public string RoomPassword { get; set; }
        public int CurrentStageNumber { get; set; }
        public int MafiaCount { get; set; }
        public int PlayerCount { get; set; }
        public int PlayerCurrentCount { get; set; }

        public List<RoomPlayer> Players { get; set; }
        public List<RoomStage> Stages { get; set; }
    }

    public enum Status
    {
        game_start = 0,
        winner_not = 1,
        citizen_win = 2,
        mafia_win = 3,
        day = 4,
        nigth = 5
    }
}
