using DailyRoutines.Application.Common;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DailyRoutines.Api.Controllers;

[Access]
public class UsersManagerController : SiteBaseController
{
    #region constractor

    private readonly IUserService _userService;
    private readonly IAccessService _accessService;

    public UsersManagerController(IUserService userService, IAccessService accessService)
    {
        _userService = userService;
        _accessService = accessService;
    }

    #endregion


    #region Get Users

    [HttpGet("[action]")]
    public IActionResult Users([FromQuery] FilterUsersDTO filter)
    {
        var users = _userService.GetUsers(filter);

        return users == null ? JsonResponseStatus.NotFound("کاربری یافت نشد.") :
            JsonResponseStatus.Success(users);
    }

    #endregion

    #region Get Admin Users

    [HttpGet("[action]")]
    public IActionResult AdminUsers([FromQuery] FilterUsersDTO filter)
    {
        var users = _accessService.GetUsersWithRole(filter);

        return users == null ? JsonResponseStatus.NotFound("کاربری یافت نشد.") :
            JsonResponseStatus.Success(users);
    }

    #endregion

    #region BLock User

    [HttpPut("[action]")]
    public IActionResult BLockUser([FromQuery] Guid userId)
    {
        if (userId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");


        var user = _userService.GetUserById(userId);

        if (user == null)
            return JsonResponseStatus.NotFound("کاربر یافت نشد.");

        user.IsBlock = true;

        var editUser = _userService.EditUser(user);

        if (editUser == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Active User

    [HttpPut("[action]")]
    public IActionResult ActiveUser([FromQuery] Guid userId)
    {
        if (userId.IsEmpty())
        {
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");
        }


        var user = _userService.GetUserById(userId);

        if (user == null)
            return JsonResponseStatus.NotFound("کاربر یافت نشد.");

        user.IsBlock = false;

        var editUser = _userService.EditUser(user);

        if (editUser == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Edit User

    [HttpGet("[action]")]
    public IActionResult EditUser([FromQuery] Guid userId)
    {
        if (userId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var data = _userService.GetUserForEdit(userId);

        if (data == null)
            JsonResponseStatus.NotFound("کاربری یافت نشد.");

        return JsonResponseStatus.Success(data);
    }

    [HttpPut("[action]")]
    public IActionResult EditUser([FromBody] EditUserDTO userData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");


        var user = _userService.GetUserById(userData.UserId);

        if (user == null)
            JsonResponseStatus.NotFound("کاربری یافت نشد.");

        user.FirstName = userData.FirstName;
        user.LastName = userData.LastName;
        user.Email = userData.Email;
        user.PhoneNumber = user.PhoneNumber;

        var editUser = _userService.EditUser(user);

        if (editUser != ResultTypes.Successful)
            return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");

        var removeRoles = _accessService.RemoveAllUserRoles(user.Id);
        if (removeRoles != ResultTypes.Successful)
            return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");


        var addRoles = _accessService.AddUserRole(user.Id, userData.Roles);
        if (addRoles != ResultTypes.Successful)
            return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");


        return JsonResponseStatus.Success();
    }

    #endregion
}