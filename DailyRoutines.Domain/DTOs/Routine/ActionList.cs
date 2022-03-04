using System;

namespace DailyRoutines.Domain.DTOs
{
    public class ActionList
    {
        public Guid ActionId { get; set; }
        public string ActionTitle { get; set; }
        public DateTime CreateDate { get; set; }
    }
}