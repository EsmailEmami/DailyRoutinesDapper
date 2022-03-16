using DailyRoutines.Domain.DTOs.Chat;
using DailyRoutines.Domain.Entities.Chat;
using DailyRoutines.Domain.Enums;

namespace DailyRoutines.Application.Interfaces;

public interface IChatRoomService
{
    MessageForShowDTO AddMessage(ChatMessage message);
}