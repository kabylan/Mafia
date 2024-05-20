using Mafia.Domain.Entities.Game;
using Mafia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Application.Services.Mafia
{
    public interface IMafiaService
    {
        Task<int> UserCreate(string userId, string roomNumber, string roomPassword, string name, int age, Gender gender, string photo);
        List<Room> ListRoom();
        Room CreateRoom(string adminId, int roomMafia, int playerCount);
        void DisablePlayer(string playerId);
        void StartGame(int roomId);
        List<PlayerStatus> GetAllPlayerStatusLive(int roomId);
        List<PlayerStatus> GetAllPlayerStatusLiveUser(int roomId);
        Task RoomStageUpdate(int roomId, RoomStageUpdateType stageUpdateType);
        void PlayerVote(int roomId, string votingPlayerId, string targetPlayerId);
        Task<GameStatus> UpdateGameStatus(int roomId);
    }

    public enum RoomStageUpdateType
    {
        StartNight,
        NightMafia,
        PostKill,
        GetKill,
        NightDoctor,
        PostMedical,
        GetMedical,
        NightWhore,
        PostPutana,
        GetPutana,
        CommisarWhore,
        PostCommisar,
        StartDay
    }

    public class PlayerStatus
    {
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string RoomNumber { get; set; }
        public bool? IsAlive { get; set; }
        public RoomRole? Role { get; set; }
    }

    public class GameStatus
    {
        public Status Status { get; set; }
        public int PlayerCount { get; set; }
    }
}
