using DailyRoutines.Application.Common;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DailyRoutines.Api.Controllers;

[Access]
public class AccessManagerController : SiteBaseController
{
    #region constractor

    private readonly IAccessService _accessService;
    private readonly IUserService _userService;

    public AccessManagerController(IAccessService accessService, IUserService userService)
    {
        _accessService = accessService;
        _userService = userService;
    }

    #endregion

    #region Get Roles

    [HttpGet("[action]")]
    public IActionResult Roles([FromQuery] FilterRolesDTO filter)
    {
        var roles = _accessService.GetRoles(filter);

        return roles == null ? JsonResponseStatus.NotFound() : JsonResponseStatus.Success(roles);
    }

    #endregion

    #region Add Role

    [HttpPost("[action]")]
    public IActionResult AddRole([FromBody] AddRoleDTO roleData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var role = new Role()
        {
            RoleName = roleData.RoleName,
            CreateDate = DateTime.Now,
            LastUpdateDate = DateTime.Now,
        };

        var addRole = _accessService.AddRole(role);

        if (addRole == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Edit Role

    [HttpGet("[action]")]
    public IActionResult EditRole([FromQuery] Guid roleId)
    {
        if (roleId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var role = _accessService.GetRoleForEdit(roleId);

        if (role == null)
            return JsonResponseStatus.NotFound("مقام یافت نشد.");


        return JsonResponseStatus.Success(role);
    }

    [HttpPut("[action]")]
    public IActionResult EditRole([FromBody] EditRoleDTO roleData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        var role = _accessService.GetRoleById(roleData.RoleId);

        if (role == null)
            return JsonResponseStatus.NotFound("مقام یافت نشد.");

        role.RoleName = roleData.RoleName;

        var editRole = _accessService.EditRole(role);

        if (editRole == ResultTypes.Successful)
            return JsonResponseStatus.Success();


        return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");
    }

    #endregion

    #region Roles For Select

    [HttpGet("[action]")]
    public IActionResult RolesForSelect()
    {
        var data = _accessService.GetRolesForSelect();

        if (!data.Any())
            return JsonResponseStatus.NotFound("مقامی برای نمایش یافت نشد.");

        return JsonResponseStatus.Success(data);
    }

    #endregion


    #region User Roles

    [HttpGet("[action]")]
    public IActionResult UserRoles([FromQuery] Guid userId, [FromQuery] FilterRolesDTO filter)
    {
        if (userId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");

        if (!_userService.IsUserExist(userId))
            return JsonResponseStatus.NotFound("کاربری یافت نشد.");

        var roles = _accessService.GetUserRoles(userId, filter);

        return roles == null ? JsonResponseStatus.NotFound("مقامی یافت نشد.") :
            JsonResponseStatus.Success(roles);
    }

    #endregion

    #region Delete User From Role

    [HttpDelete("[action]")]
    public IActionResult DeleteRoleFromUser([FromQuery] Guid userId, [FromQuery] Guid roleId)
    {
        if (roleId.IsEmpty() || userId.IsEmpty())
            return JsonResponseStatus.Error("اطلاعات وارد شده نادرست است.");


        var deleteUserRole = _accessService.DeleteUserRole(userId, roleId);

        if (deleteUserRole != ResultTypes.Successful)
            return JsonResponseStatus.Error("متاسفانه مشکلی پیش آمده است! لطفا دوباره تلاش کنید.");

        return JsonResponseStatus.Success();

    }

    #endregion
}