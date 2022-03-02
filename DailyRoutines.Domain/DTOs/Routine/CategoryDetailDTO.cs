using System;

namespace DailyRoutines.Domain.DTOs.Routine;

public class CategoryDetailDTO
{
    public Guid CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public string LastUpdate { get; set; }
    public int ActionsCount { get; set; }
}