using DocumentFormat.OpenXml.InkML;
using Mafia.Application.Services.Interfaces;
using Mafia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Application.Services
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Hub
    {
        public async Task AddToRoleGroup(string userId, string roomNumber, string role)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{roomNumber}_{role}");
        }

        public async Task RemoveFromRoleGroup(string userId, string roomNumber, string role)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{roomNumber}_{role}");
        }

        public async Task SendUserConnected(string userId, string roomNumber)
        {
            await Clients.Group(roomNumber).SendAsync("UserConnected", userId);
        }


        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var roomNumber = httpContext.Request.Query["roomNumber"];

            await Groups.AddToGroupAsync(Context.ConnectionId, roomNumber);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var roomNumber = httpContext.Request.Query["roomNumber"];

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomNumber);
            await base.OnDisconnectedAsync(exception);
        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            return connection.User.Identity.Name;
        }
    }

    public class ConnectedUsersManager
    {
        private readonly Dictionary<string, string> connectedUsers = new Dictionary<string, string>();

        public void AddConnectedUser(string userId, string connectionId)
        {
            // Добавляем пользователя в словарь
            connectedUsers[userId] = connectionId;
        }

        public void RemoveConnectedUser(string userId)
        {
            // Удаляем пользователя из словаря
            connectedUsers.Remove(userId);
        }

        public string GetConnectionId(string userId)
        {
            // Получаем идентификатор соединения по идентификатору пользователя
            return connectedUsers.TryGetValue(userId, out var connectionId) ? connectionId : null;
        }

        public IEnumerable<string> GetAllConnectedUsers()
        {
            // Получаем все идентификаторы подключенных пользователей
            return connectedUsers.Keys;
        }
    }
}