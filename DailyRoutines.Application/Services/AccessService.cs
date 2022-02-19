using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Enums;
using DailyRoutines.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using DailyRoutines.Domain.DTOs.Common;

namespace DailyRoutines.Application.Services;

public class AccessService : IAccessService
{
    private readonly IAccessRepository _role;

    public AccessService(IAccessRepository role)
    {
        _role = role;
    }

    public FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter) =>
        _role.GetUsersWithRole(filter);

    public FilterRolesDTO GetRoles(FilterRolesDTO filter) =>
        _role.GetRoles(filter);

    public EditRoleDTO GetRoleForEdit(Guid roleId) =>
        _role.GetRoleForEdit(roleId);

    public Role GetRoleById(Guid roleId) =>
        _role.GetRoleById(roleId);

    public ResultTypes AddRole(Role role)
    {
        try
        {
            _role.AddRole(role);
            _role.SaveChanges();

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ResultTypes EditRole(Role role)
    {
        try
        {
            _role.UpdateRole(role);
            _role.SaveChanges();

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public List<ItemsForSelectDTO> GetRolesForSelect() =>
        _role.GetRolesForSelect();

    public ResultTypes RemoveAllUserRoles(Guid userId)
    {
        try
        {
            var userRoles = _role.GetUserRoles(userId);

            if (!userRoles.Any())
                return ResultTypes.Successful;


            foreach (var userRole in userRoles)
            {
                _role.RemoveUserRole(userRole);
            }


            _role.SaveChanges();

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ResultTypes AddUserRole(Guid userId, List<Guid> roles)
    {
        try
        {
            foreach (var role in roles)
            {
                var userRole = new UserRole()
                {
                    UserId = userId,
                    RoleId = role
                };

                _role.AddUserRole(userRole);
            }


            _role.SaveChanges();

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }
}