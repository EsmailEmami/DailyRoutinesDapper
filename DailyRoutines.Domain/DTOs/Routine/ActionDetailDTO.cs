using System;

namespace DailyRoutines.Domain.DTOs.Routine;

public class ActionDetailDTO
{
    public ActionDetailDTO(Guid actionId, string actionTitle, string actionDescription, string createDate, string lastUpdateDate)
    {
        ActionId = actionId;
        ActionTitle = actionTitle;
        ActionDescription = actionDescription;
        CreateDate = createDate;
        LastUpdateDate = lastUpdateDate;
    }


    public Guid ActionId { get; set; }
    public string ActionTitle { get; set; }
    public string ActionDescription { get; set; }
    public string CreateDate { get; set; }
    public string LastUpdateDate { get; set; }
}