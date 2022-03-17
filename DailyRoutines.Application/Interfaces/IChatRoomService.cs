using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Chat;
using DailyRoutines.Domain.Entities.Chat;
using DailyRoutines.Domain.Enums;

namespace DailyRoutines.Application.Interfaces;

public interface IChatRoomService
{
    List<MessageForShowDTO> GetUserMessagesHistory(Guid fromUser, Guid toUser);
    MessageForShowDTO AddMessage(ChatMessage message);
}