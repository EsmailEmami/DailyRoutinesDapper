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

    public async Task UserChatHistory(Guid userId)
    {
        var currentUserId = Context.User.GetUserId();
        var history = _chatRoomService.GetUserMessagesHistory(currentUserId, userId);

        if (history != null)
        {
            await Clients.Caller.SendAsync("ReceiveChatHistory", history);
        }
    }

    public async Task SendMessageToUser(AddMessageDTO message)
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
            await Clients.Caller.SendAsync("ReceiveUserMessage", messageResult);

            messageResult.YouSent = false;

            await Clients.User(message.ToUser.ToString()).SendAsync("ReceiveUserMessage", messageResult);
        }
    }
}