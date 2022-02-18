using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using System;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);

        bool IsUserExist(string email, string password);
        User GetUserByEmail(string email);
        User GetUserById(Guid userId);
        bool IsUserPhoneNumberExists(string phoneNumber);
        bool IsUserEmailExists(string email);

        UserDashboardDTO GetUserDashboard(Guid userId);

        void SaveChanges();
    }
}