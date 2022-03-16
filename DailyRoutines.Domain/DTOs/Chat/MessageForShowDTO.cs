using System;

namespace DailyRoutines.Domain.DTOs.Chat;

public class MessageForShowDTO
{
    public int MessageId { get; set; }
    public string Message { get; set; }
    public DateTime SendAt { get; set; }
}