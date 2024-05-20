using Mafia.Application.Services;
using Mafia.Application.Services.Mafia;
using Mafia.Domain.Entities.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mafia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MafiaController : ControllerBase
    {
        private readonly IMafiaService _mafiaService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MafiaController(IMafiaService mafiaService, IHubContext<ChatHub> hubContext)
        {
            _mafiaService = mafiaService;
            _hubContext = hubContext;
        }

        // POST: api/Mafia/UserCreate
        [HttpPost("UserCreate")]
        public async Task<ActionResult<int>> UserCreate([FromBody] UserCreateRequest request)
        {
            var userId = await _mafiaService.UserCreate(request.UserId, request.RoomNumber, request.RoomPassword, request.Name, request.Age, request.Gender, request.Photo);
            await _hubContext.Clients.Group(request.RoomNumber).SendAsync("UserConnected", userId);
            return Ok(userId);
        }

        // GET: api/Mafia/ListRoom
        [HttpGet("ListRoom")]
        public ActionResult<List<Room>> ListRoom()
        {
            var rooms = _mafiaService.ListRoom();
            return Ok(rooms);
        }

        // POST: api/Mafia/CreateRoom
        [HttpPost("CreateRoom")]
        public ActionResult<Room> CreateRoom([FromBody] CreateRoomRequest request)
        {
            var room = _mafiaService.CreateRoom(request.AdminId, request.RoomMafia, request.PlayerCount);
            return Ok(room);
        }

        // PUT: api/Mafia/DisablePlayer
        [HttpPut("DisablePlayer")]
        public IActionResult DisablePlayer([FromBody] DisablePlayerRequest request)
        {
            _mafiaService.DisablePlayer(request.PlayerId);
            return Ok();
        }

        // POST: api/Mafia/StartGame
        [HttpPost("StartGame")]
        public async Task<IActionResult> StartGameAsync([FromBody] int roomId)
        {
            _mafiaService.StartGame(roomId);
            // Получаем всех игроков и их роли в комнате
            var playerStatuses = _mafiaService.GetAllPlayerStatusLive(roomId);

            foreach (var playerStatus in playerStatuses)
            {
                // Добавляем игроков в группы в зависимости от их ролей
                await _hubContext.Clients.User(playerStatus.PlayerId).SendAsync("AddToRoleGroup", playerStatus.PlayerId, playerStatus.RoomNumber, playerStatus.Role);

                // Уведомляем игроков о начале игры и их роли
                await _hubContext.Clients.User(playerStatus.PlayerId).SendAsync("GameStarted", playerStatus.Role);
            }
            return Ok();
        }

        // GET: api/Mafia/GetAllPlayerStatusLive/{roomId}
        [HttpGet("GetAllPlayerStatusLive/{roomId}")]
        public ActionResult<List<PlayerStatus>> GetAllPlayerStatusLive(int roomId)
        {
            var statuses = _mafiaService.GetAllPlayerStatusLive(roomId);
            return Ok(statuses);
        }

        // GET: api/Mafia/GetAllPlayerStatusLiveUser/{roomId}
        [HttpGet("GetAllPlayerStatusLiveUser/{roomId}")]
        public ActionResult<List<PlayerStatus>> GetAllPlayerStatusLiveUser(int roomId)
        {
            var statuses = _mafiaService.GetAllPlayerStatusLiveUser(roomId);
            return Ok(statuses);
        }

        // PUT: api/Mafia/RoomStageUpdate
        [HttpPut("RoomStageUpdate")]
        public IActionResult RoomStageUpdate([FromBody] RoomStageUpdateRequest request)
        {
            _mafiaService.RoomStageUpdate(request.RoomId, request.StageUpdateType);
            return Ok();
        }

        // POST: api/Mafia/PlayerVote
        [HttpPost("PlayerVote")]
        public IActionResult PlayerVote([FromBody] PlayerVoteRequest request)
        {
            _mafiaService.PlayerVote(request.RoomId, request.VotingPlayerId, request.TargetPlayerId);
            return Ok();
        }

        // GET: api/Mafia/UpdateGameStatus/{roomId}
        [HttpGet("UpdateGameStatus/{roomId}")]
        public ActionResult<GameStatus> UpdateGameStatus(int roomId)
        {
            var status = _mafiaService.UpdateGameStatus(roomId);
            return Ok(status);
        }
    }

    // Пример моделей запросов
    public class UserCreateRequest
    {
        public string UserId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomPassword { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Photo { get; set; }
    }

    public class CreateRoomRequest
    {
        public string AdminId { get; set; }
        public int RoomMafia { get; set; }
        public int PlayerCount { get; set; }
    }

    public class DisablePlayerRequest
    {
        public string PlayerId { get; set; }
    }

    public class RoomStageUpdateRequest
    {
        public int RoomId { get; set; }
        public RoomStageUpdateType StageUpdateType { get; set; }
    }

    public class PlayerVoteRequest
    {
        public int RoomId { get; set; }
        public string VotingPlayerId { get; set; }
        public string TargetPlayerId { get; set; }
    }
}
