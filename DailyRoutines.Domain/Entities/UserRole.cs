using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutines.Domain.Entities
{
    public class UserRole
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}