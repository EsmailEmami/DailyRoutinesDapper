using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using System;
using System.Collections.Generic;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);

        bool IsUserExist(string email, string password);
        bool IsUserExist(Guid userId);
        User GetUserByEmail(string email);
        User GetUserById(Guid userId);
        bool IsUserPhoneNumberExists(string phoneNumber);
        bool IsUserEmailExists(string email);

        int GetProductsCount(string type, string filter);

        UserDashboardDTO GetUserDashboard(Guid userId);
        EditUserDTO GetUserForEdit(Guid userId);

        UserInformationDTO GetUserInformation(Guid userId);

        List<UsersListDTO> GetUsers(int skip, int take, string type, string filter);

    }
}