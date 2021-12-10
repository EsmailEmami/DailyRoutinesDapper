using System;
using System.Collections.Generic;

namespace DailyRoutines.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<UserCategory> UserCategories { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}