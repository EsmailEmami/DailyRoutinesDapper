using System;
using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.Entities.Chat;

public class ChatMessage
{
    public Guid FromUser { get; set; }
    public Guid ToUser { get; set; }
    public int MessageId { get; set; }

    [Required]
    [MaxLength(4000)]
    public string Message { get; set; }
    public DateTime SendAt { get; set; }
}