using System;

namespace DailyRoutines.Domain.DTOs.Routine;

public class ActionDetailDTO
{
    public Guid ActionId { get; set; }
    public string ActionTitle { get; set; }
    public string ActionDescription { get; set; }
    public string CreateDate { get; set; }
    public string LastUpdateDate { get; set; }
}