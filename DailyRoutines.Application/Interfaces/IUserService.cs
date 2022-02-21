using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Enums;
using System;

namespace DailyRoutines.Application.Interfaces;

public interface IUserService
{
    ResultTypes AddUser(User user);
    ResultTypes EditUser(User user);

    bool IsUserExist(string email, string password);
    bool IsUserExist(Guid userId);
    User GetUserByEmail(string email);
    User GetUserById(Guid userId);
    bool IsUserPhoneNumberExists(string phoneNumber);
    bool IsUserEmailExists(string email);

    FilterUsersDTO GetUsers(FilterUsersDTO filter);
    EditUserDTO GetUserForEdit(Guid userId);

    UserInformationDTO GetUserInformation(Guid userId);

    UserDashboardDTO GetUserDashboard(Guid userId);
}