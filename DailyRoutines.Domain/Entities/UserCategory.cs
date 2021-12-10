using System;

namespace DailyRoutines.Domain.Entities
{
    public class UserCategory
    {
        public Guid UserCategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
