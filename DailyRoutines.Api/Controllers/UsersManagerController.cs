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
        {
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");
        }


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
}