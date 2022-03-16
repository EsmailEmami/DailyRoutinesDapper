using System;
using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.DTOs.Chat;

public class AddMessageDTO
{
    [Required]
    public Guid ToUser { get; set; }

    [Required]
    [MaxLength(4000)]
    public string Message { get; set; }
}