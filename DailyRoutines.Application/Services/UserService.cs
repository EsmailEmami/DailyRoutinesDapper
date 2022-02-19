using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Enums;
using DailyRoutines.Domain.Interfaces;
using System;

namespace DailyRoutines.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _user;

    public UserService(IUserRepository user)
    {
        _user = user;
    }

    public ResultTypes AddUser(User user)
    {
        try
        {
            user.Password = PasswordHelper.EncodePasswordMd5(user.Password);

            _user.AddUser(user);
            _user.SaveChanges();

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ResultTypes EditUser(User user)
    {
        try
        {
            _user.UpdateUser(user);
            _user.SaveChanges();

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public bool IsUserExist(string email, string password)
    {
        password = PasswordHelper.EncodePasswordMd5(password);

        return _user.IsUserExist(email, password);
    }

    public User GetUserByEmail(string email) =>
        _user.GetUserByEmail(email.Fixed());

    public User GetUserById(Guid userId) =>
        _user.GetUserById(userId);

    public bool IsUserPhoneNumberExists(string phoneNumber) =>
        _user.IsUserPhoneNumberExists(phoneNumber);

    public bool IsUserEmailExists(string email) =>
        _user.IsUserEmailExists(email);

    public FilterUsersDTO GetUsers(FilterUsersDTO filter) =>
        _user.GetUsers(filter);

    public UserDashboardDTO GetUserDashboard(Guid userId) =>
        _user.GetUserDashboard(userId);
}