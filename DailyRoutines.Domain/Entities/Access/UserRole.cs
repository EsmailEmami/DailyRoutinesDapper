using System;
using System.ComponentModel.DataAnnotations.Schema;
using DailyRoutines.Domain.Entities.Access;

namespace DailyRoutines.Domain.Entities.Access
{
    public class UserRole
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public User.User User { get; set; }
        public Role Role { get; set; }
    }
}