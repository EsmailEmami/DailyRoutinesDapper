using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.Chat;
using DailyRoutines.Domain.Entities.Chat;
using Microsoft.AspNetCore.SignalR;

namespace DailyRoutines.Api.Hubs;

public class ChatHub : Hub
{
    private readonly IChatRoomService _chatRoomService;

    public ChatHub(IChatRoomService chatRoomService)
    {
        _chatRoomService = chatRoomService;
    }

    public Task SendMessageToUser(AddMessageDTO message)
    {
        var newMessage = new ChatMessage()
        {
            FromUser = Context.User.GetUserId(),
            ToUser = message.ToUser,
            Message = message.Message
        };

        var messageResult = _chatRoomService.AddMessage(newMessage);

        if (messageResult != null)
        {
            Clients.Caller.SendAsync("ReceiveCallerMessage", messageResult);

            Clients.User(message.ToUser.ToString()).SendAsync("ReceiveUserMessage", messageResult);
        }

        return Task.CompletedTask;
    }
}