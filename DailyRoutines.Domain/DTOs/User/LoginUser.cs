using System;

namespace DailyRoutines.Domain.DTOs.User
{
    public class LoginUser
    {
        public LoginUser(Guid userId, string firstName, string lastName, int expireTime, string token)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ExpireTime = expireTime;
            Token = token;
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ExpireTime { get; set; }
        public string Token { get; set; }
    }
}