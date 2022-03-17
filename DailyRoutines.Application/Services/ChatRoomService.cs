using System;
using System.Collections.Generic;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.Chat;
using DailyRoutines.Domain.Entities.Chat;
using DailyRoutines.Domain.Interfaces;

namespace DailyRoutines.Application.Services;

public class ChatRoomService : IChatRoomService
{
    private readonly IChatRoomRepository _chatRoom;

    public ChatRoomService(IChatRoomRepository chatRoom)
    {
        _chatRoom = chatRoom;
    }

    public List<MessageForShowDTO> GetUserMessagesHistory(Guid fromUser, Guid toUser)
    {
        return _chatRoom.GetUserMessagesHistory(fromUser, toUser);
    }

    public MessageForShowDTO AddMessage(ChatMessage message)
    {
        return _chatRoom.AddMessage(message);
    }
}