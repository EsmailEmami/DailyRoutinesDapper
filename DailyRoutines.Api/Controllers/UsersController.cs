using DailyRoutines.Application.Common;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DailyRoutines.Api.Controllers;

[Access]
public class UsersController : SiteBaseController
{
    #region constructor

    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion


    #region Dashboard

    [HttpGet("[action]")]
    public IActionResult DashBoard()
    {
        var dashboardData = _userService.GetUserDashboard(User.GetUserId());

        if (dashboardData == null)
            return JsonResponseStatus.NotFound();


        return JsonResponseStatus.Success(dashboardData);
    }

    #endregion

    #region Edit Dashboard

    [HttpPut("[action]")]
    public IActionResult EditDashboard([FromBody] EditUserDashboardDTO userData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var user = _userService.GetUserById(User.GetUserId());

        if (user == null)
            return JsonResponseStatus.NotFound("کاربری یافت نشد.");

        user.FirstName = userData.FirstName;
        user.LastName = userData.LastName;

        var editUser = _userService.EditUser(user);

        if (editUser == ResultTypes.Successful)
            return JsonResponseStatus.Success();

        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Edit Dashboard

    [HttpPost("[action]")]
    public IActionResult AddPhoneNumber([FromQuery] string phoneNumber)
    {
        var user = _userService.GetUserById(User.GetUserId());

        if (user == null)
            return JsonResponseStatus.NotFound("کاربری یافت نشد.");

        if (user.PhoneNumber != null)
            return JsonResponseStatus.Error("شماره تلفن شما از قبل ثبت شده است.");


        user.PhoneNumber = phoneNumber;

        var editUser = _userService.EditUser(user);

        if (editUser == ResultTypes.Successful)
            return JsonResponseStatus.Success();

        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion
}