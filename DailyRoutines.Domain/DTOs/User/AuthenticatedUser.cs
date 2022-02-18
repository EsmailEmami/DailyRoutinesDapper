using System;

namespace DailyRoutines.Domain.DTOs.User
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(Guid userId, string firstName, string lastName, string email)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }


        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}