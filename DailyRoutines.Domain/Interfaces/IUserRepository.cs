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
        bool IsUserExist(Guid userId);
        User GetUserByEmail(string email);
        User GetUserById(Guid userId);
        bool IsUserPhoneNumberExists(string phoneNumber);
        bool IsUserEmailExists(string email);

        UserDashboardDTO GetUserDashboard(Guid userId);
        EditUserDTO GetUserForEdit(Guid userId);

        UserInformationDTO GetUserInformation(Guid userId);

        FilterUsersDTO GetUsers(FilterUsersDTO filter);

        void SaveChanges();
    }
}