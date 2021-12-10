using System;

namespace DailyRoutines.Domain.Entities
{
    public class Action
    {
        public Guid ActionId { get; set; }

        public string ActionTitle { get; set; }

        public DateTime CreateDate { get; set; }

        public string Description { get; set; }

        public UserCategory UserCategory { get; set; }
    }
}
