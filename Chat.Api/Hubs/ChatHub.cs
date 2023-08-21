using System;
using System.Threading.Tasks;
using Chat.Api.Dtos;
using Microsoft.AspNetCore.SignalR;
using Persistence.Interfaces;

namespace Chat.Api.Hubs;

public class ChatHub : Hub
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;
    
    public ChatHub(IUserRepository userUserRepository, IMessageRepository messageRepository)
    {
        _userRepository = userUserRepository;
        _messageRepository = messageRepository;
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "RequiemChat");
        await Clients.Caller.SendAsync("UserConnected");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "RequiemChat");
        var user = _userRepository.GetUserByConnectionId(Context.ConnectionId);
        _userRepository.RemoveUser(user);
        await DisplayOnlineUsers();
        
        await base.OnDisconnectedAsync(exception);
    }

    public async Task AddUserConnectionId(string name)
    {
        _userRepository.AddUserConnectionId(name, Context.ConnectionId);
        await DisplayOnlineUsers();
    }

    public async Task ReceiveMessage(MessageDto message)
    {
        await Clients.Group("RequiemChat").SendAsync("NewMessage", message);
    }

    public async Task CreatePrivateChat(MessageDto message)
    {
        var privateGroupName = GetPrivateGroupName(message.From, message.To);
        await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
        var toConnectionId = _userRepository.GetConnectionIdByUser(message.To);
        await Groups.AddToGroupAsync(toConnectionId, privateGroupName);
        
        _messageRepository.AddPrivateMessage(message.From, message.To, message.Content);
        
        await Clients.Client(toConnectionId).SendAsync("OpenPrivateChat", message);
    }

    public async Task ReceivePrivateMessage(MessageDto message)
    {
        var privateGroupName = GetPrivateGroupName(message.From, message.To);
        await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", message);
    }

    public async Task RemovePrivateChat(string from, string to)
    {
        var privateGroupName = GetPrivateGroupName(from, to);
        await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
        var toConnectionId = _userRepository.GetConnectionIdByUser(to);
        await Groups.RemoveFromGroupAsync(toConnectionId, privateGroupName);
    }

    private async Task DisplayOnlineUsers()
    {
        var onlineUsers = _userRepository.GetOnlineUsers();
        await Clients.Group("RequiemChat").SendAsync("OnlineUsers", onlineUsers);
    }

    private string GetPrivateGroupName(string from, string to)
    {
        var stringCompare = string.CompareOrdinal(from, to) < 0;
        return stringCompare ? $"{from}-{to}" : $"{to}-{from}";
    }
}