﻿using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Chat;
using DailyRoutines.Domain.Entities.Chat;

namespace DailyRoutines.Domain.Interfaces;

public interface IChatRoomRepository
{
    MessageForShowDTO AddMessage(ChatMessage message);
    List<MessageForShowDTO> GetUserMessagesHistory(Guid fromUser, Guid toUser);
}