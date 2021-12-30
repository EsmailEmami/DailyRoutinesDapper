using System;

namespace DailyRoutines.Domain.DTOs
{
    public class ActionList
    {
        public ActionList(Guid actionId, string actionTitle, DateTime createDate)
        {
            ActionId = actionId;
            ActionTitle = actionTitle;
            CreateDate = createDate;
        }

        public Guid ActionId { get; set; }
        public string ActionTitle { get; set; }
        public DateTime CreateDate { get; set; }
    }
}