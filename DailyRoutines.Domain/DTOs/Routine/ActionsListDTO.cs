using System;

namespace DailyRoutines.Domain.DTOs.Routine;

public class ActionsListDTO
{
    public Guid ActionId { get; set; }
    public string ActionTitle { get; set; }
    public string CreateDate { get; set; }
}