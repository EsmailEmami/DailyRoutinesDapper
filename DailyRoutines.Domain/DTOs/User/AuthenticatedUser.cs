using System;

namespace DailyRoutines.Domain.DTOs.User
{
    public class AuthenticatedUser
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}