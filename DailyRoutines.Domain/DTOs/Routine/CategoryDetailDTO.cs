using System;

namespace DailyRoutines.Domain.DTOs.Routine;

public class CategoryDetailDTO
{
    public CategoryDetailDTO(Guid categoryId, string categoryTitle, string lastUpdate, int actionsCount)
    {
        CategoryId = categoryId;
        CategoryTitle = categoryTitle;
        LastUpdate = lastUpdate;
        ActionsCount = actionsCount;
    }

    public Guid CategoryId { get; set; }
    public string CategoryTitle   { get; set; }
    public string LastUpdate { get; set; }
    public int ActionsCount { get; set; }
}