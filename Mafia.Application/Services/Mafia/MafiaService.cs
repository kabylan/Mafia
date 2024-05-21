using Mafia.Domain.Data.Adapters;
using Mafia.Domain.Entities;
using Mafia.Domain.Entities.Game;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Application.Services.Mafia
{
    public class MafiaService : IMafiaService
    {
        private readonly MafiaDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public MafiaService(MafiaDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }


        public async Task<int> UserCreate(string userId, string roomNumber, string roomPassword, string name, int age, Gender gender, string photo)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(e => e.RoomPassword == roomPassword && e.RoomNumber == roomNumber);
            if (room == null)
            {
                throw new InvalidOperationException("Room not found");
            }
            var roomP = new RoomPlayer
            {
                PlayerName = name,
                PlayerAge = age,
                PlayerGender = gender,
                PlayerPhoto = photo,
                RoomId = room.Id,
                PlayerId = userId
            };

            _context.RoomPlayers.Add(roomP);
            _context.SaveChanges();

            return room.Id;
        }

        public List<Room> ListRoom()
        {
            return _context.Rooms.Include(r => r.Players).Include(r => r.Stages).ToList();
        }

        string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }

        public Room CreateRoom(string adminId, int roomMafia, int playerCount)
        {

            var room = new Room
            {
                UserId = adminId,
                RoomNumber = GenerateRandomText(8),
                RoomPassword = GenerateRandomText(8),
                CreateDate = DateTime.UtcNow,
                Status = Status.game_start,
                CurrentStageNumber = 1,
                MafiaCount = roomMafia,
                PlayerCount = playerCount,
                PlayerCurrentCount = 0,
                Players = new List<RoomPlayer>(),
                Stages = new List<RoomStage>()
            };

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return room;
        }

        public void DisablePlayer(string playerId)
        {
            var player = _context.RoomPlayers.FirstOrDefault(e => e.PlayerId == playerId && (e.Room.Status != Status.mafia_win || e.Room.Status != Status.citizen_win));
            if (player != null)
            {
                player.RoomEnabled = false;
                _context.RoomPlayers.Update(player);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Player not found");
            }
        }

        public void StartGame(int roomId)
        {
            var room = _context.Rooms.Include(r => r.Players).FirstOrDefault(r => r.Id == roomId);
            if (room != null && room.Players.Count == room.PlayerCount)
            {
                var roles = new List<RoomRole>();

                // Add the specified number of mafia roles
                for (int i = 0; i < room.MafiaCount; i++)
                {
                    roles.Add(RoomRole.Mafia);
                }

                // Add one of each special role
                roles.Add(RoomRole.Commisar);
                roles.Add(RoomRole.Doctor);
                roles.Add(RoomRole.Putana);

                // Fill the remaining roles with civilians
                while (roles.Count < room.Players.Count)
                {
                    roles.Add(RoomRole.Civilian);
                }

                var random = new Random();
                foreach (var player in room.Players)
                {
                    int index = random.Next(roles.Count);
                    player.RoomRole = roles[index];
                    roles.RemoveAt(index);
                }

                room.Status = Status.day;
                room.CurrentStageNumber = 1;

                var stage = new RoomStage
                {
                    RoomId = roomId,
                    Day = true,
                    Stage = room.CurrentStageNumber
                };

                room.Stages.Add(stage);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Room not found or player count does not match the required player count");
            }
        }

        public List<PlayerStatus> GetAllPlayerStatusLive(int roomId)
        {
            var room = _context.Rooms.Include(r => r.Players).FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                return room.Players.Select(p => new PlayerStatus
                {
                    PlayerId = p.PlayerId,
                    PlayerName = p.PlayerName,
                    IsAlive = p.RoomEnabled,
                    Role = p.RoomRole,
                    RoomNumber = p.Room.RoomNumber
                }).ToList();
            }
            else
            {
                throw new InvalidOperationException("Room not found");
            }
        }

        public List<PlayerStatus> GetAllPlayerStatusLiveUser(int roomId)
        {
            var room = _context.Rooms.Include(r => r.Players).FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                return room.Players.Select(p => new PlayerStatus
                {
                    PlayerId = p.PlayerId,
                    PlayerName = p.PlayerName,
                    IsAlive = p.RoomEnabled,
                }).ToList();
            }
            else
            {
                throw new InvalidOperationException("Room not found");
            }
        }

        public async Task RoomStageUpdate(int roomId, RoomStageUpdateType stageUpdateType)
        {
            var room = _context.Rooms.Include(r => r.Stages).FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                throw new InvalidOperationException("Room not found");
            }

            var currentStage = room.Stages.OrderByDescending(e => e.Stage).FirstOrDefault();

            // Функция для проверки завершения всех этапов ночи
            bool IsNightComplete(RoomStage stage)
            {
                return stage.Nigth && stage.Mafia && stage.Doctor && stage.Putana && stage.Commisar_whore;
            }

            // Если начинается ночь, и все предыдущие ночные этапы завершены, создаем новую стадию
            if (stageUpdateType == RoomStageUpdateType.StartNight)
            {
                if (currentStage != null && IsNightComplete(currentStage))
                {
                    var newStage = new RoomStage
                    {
                        Stage = currentStage.Stage + 1
                        // Инициализируем другие поля, если нужно
                    };
                    room.Stages.Add(newStage);
                    currentStage = newStage;
                }
                else if (currentStage == null || !IsNightComplete(currentStage))
                {
                    throw new InvalidOperationException("Cannot start a new night until the previous night stages are completed.");
                }
            }

            switch (stageUpdateType)
            {
                case RoomStageUpdateType.StartNight:
                    currentStage.Nigth = true;
                    room.Status = Status.nigth;
                    await NotifyPlayers(roomId, "Night has started.");
                    break;
                case RoomStageUpdateType.NightMafia:
                    currentStage.Mafia = true;
                    await NotifyRole(roomId, "Mafia", "It's your turn.");
                    // Implement mafia logic
                    break;
                case RoomStageUpdateType.NightDoctor:
                    currentStage.Doctor = true;
                    await NotifyRole(roomId, "Doctor", "It's your turn.");
                    // Implement doctor logic
                    break;
                case RoomStageUpdateType.NightWhore:
                    currentStage.Putana = true;
                    await NotifyRole(roomId, "Whore", "It's your turn.");
                    // Implement whore logic
                    break;
                case RoomStageUpdateType.CommisarWhore:
                    currentStage.Commisar_whore = true;
                    await NotifyRole(roomId, "CommissarWhore", "It's your turn.");
                    // Implement commisar whore logic
                    break;
                case RoomStageUpdateType.StartDay:
                    if (currentStage != null && IsNightComplete(currentStage))
                    {
                        currentStage.Day = true;
                        room.Status = Status.day;
                        await NotifyPlayers(roomId, "Day has started.");
                        // Return statuses of players to all
                    }
                    else
                    {
                        throw new InvalidOperationException("Cannot start the day until the night stages are completed.");
                    }
                    break;
            }

            _context.SaveChanges();
        }

        private async Task NotifyPlayers(int roomId, string message)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", message);
        }

        private async Task NotifyRole(int roomId, string role, string message)
        {
            await _hubContext.Clients.Group($"{roomId}_{role}").SendAsync("ReceiveMessage", message);
        }

        public void PlayerVote(int roomId, string votingPlayerId, string targetPlayerId)
        {
            var room = _context.Rooms.Include(r => r.Players).FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                // Implement voting logic
            }
            else
            {
                throw new InvalidOperationException("Room not found");
            }
        }

        public async Task<GameStatus> UpdateGameStatus(int roomId)
        {
            var room = _context.Rooms.Include(r => r.Players).FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                var mafiaCount = room.Players.Count(p => p.RoomRole == RoomRole.Mafia && !p.RoomEnabled);
                var civilianCount = room.Players.Count(p => p.RoomRole != RoomRole.Mafia && !p.RoomEnabled);

                if (mafiaCount == 0)
                {
                    await NotifyPlayers(roomId, "The game has ended. Citizens win!");
                    return new GameStatus
                    {
                        Status = Status.citizen_win,
                        PlayerCount = civilianCount
                    };
                }
                else if (mafiaCount >= civilianCount)
                {
                    await NotifyPlayers(roomId, "The game has ended. Mafia wins!");
                    return new GameStatus
                    {
                        Status = Status.mafia_win,
                        PlayerCount = mafiaCount
                    };
                }
                else
                {
                    return new GameStatus
                    {
                        Status = Status.winner_not,
                        PlayerCount = room.Players.Count
                    };
                }
            }
            else
            {
                throw new InvalidOperationException("Room not found");
            }
        }
    }
}
