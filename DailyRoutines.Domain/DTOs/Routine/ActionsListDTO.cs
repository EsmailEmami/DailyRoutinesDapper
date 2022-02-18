using System;

namespace DailyRoutines.Domain.DTOs.Routine;

public class ActionsListDTO
{
    public ActionsListDTO(Guid actionId, string actionTitle, string createDate)
    {
        ActionId = actionId;
        ActionTitle = actionTitle;
        CreateDate = createDate;
    }

    public Guid ActionId { get; set; }
    public string ActionTitle { get; set; }
    public string CreateDate { get; set; }
}