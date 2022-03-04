using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Enums;
using DailyRoutines.Domain.Interfaces;
using System;
using DailyRoutines.Application.Generator;

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

    public bool IsUserExist(Guid userId)
    {
        return !userId.IsEmpty() && _user.IsUserExist(userId);
    }

    public User GetUserByEmail(string email)
    {
        return string.IsNullOrEmpty(email) ? null : _user.GetUserByEmail(email.Fixed());
    }

    public User GetUserById(Guid userId)
    {
        return userId.IsEmpty() ? null : _user.GetUserById(userId);
    }

    public bool IsUserPhoneNumberExists(string phoneNumber) =>
        _user.IsUserPhoneNumberExists(phoneNumber);

    public bool IsUserEmailExists(string email)
    {
        return !string.IsNullOrEmpty(email) && _user.IsUserEmailExists(email.Fixed());
    }

    public FilterUsersDTO GetUsers(FilterUsersDTO filter)
    {
        int count = _user.GetUsersCount(filter.Type, filter.Search);


        int pagesCount = (int)Math.Ceiling(count / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var data = _user.GetUsers(pager.SkipEntity, pager.TakeEntity, filter.Type, filter.Search);

        return filter.SetItems(data)
            .SetPaging(pager);
    }


    public EditUserDTO GetUserForEdit(Guid userId) =>
        _user.GetUserForEdit(userId);

    public UserInformationDTO GetUserInformation(Guid userId)
    {
        return userId.IsEmpty() ? null : _user.GetUserInformation(userId);
    }
        

    public UserDashboardDTO GetUserDashboard(Guid userId)
    {
        return userId.IsEmpty() ? null : _user.GetUserDashboard(userId);
    }
}