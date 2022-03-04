using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Enums;
using DailyRoutines.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Generator;
using DailyRoutines.Domain.DTOs.Common;

namespace DailyRoutines.Application.Services;

public class AccessService : IAccessService
{
    private readonly IAccessRepository _role;

    public AccessService(IAccessRepository role)
    {
        _role = role;
    }

    public FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter)
    {
        int count = _role.GetUsersWithRoleCount(filter.Search);

        int pagesCount = (int)Math.Ceiling(count / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var data = _role.GetUsersWithRole(pager.SkipEntity, pager.TakeEntity, filter.Type.Fixed(), filter.Search);

        return filter.SetItems(data)
            .SetPaging(pager);
    }

    public FilterRolesDTO GetRoles(FilterRolesDTO filter)
    {
        int count = _role.GetRolesCount(filter.Search);


        int pagesCount = (int)Math.Ceiling(count / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var data = _role.GetRoles(pager.SkipEntity, pager.TakeEntity, filter.Search);

        return filter.SetItems(data)
            .SetPaging(pager);
    }

    public FilterRolesDTO GetUserRoles(Guid userId, FilterRolesDTO filter)
    {
        int count = _role.GetUserRolesCount(userId, filter.Search);

        int pagesCount = (int)Math.Ceiling(count / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var data = _role.GetUserRoles(userId, pager.SkipEntity, pager.TakeEntity, filter.Search);

        return filter.SetItems(data)
            .SetPaging(pager);
    }

    public EditRoleDTO GetRoleForEdit(Guid roleId) =>
        _role.GetRoleForEdit(roleId);

    public Role GetRoleById(Guid roleId) =>
        _role.GetRoleById(roleId);

    public ResultTypes AddRole(Role role)
    {
        try
        {
            _role.AddRole(role);

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

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public ResultTypes DeleteUserRole(Guid userId, Guid roleId)
    {
        try
        {
            var userRole = _role.GetUserRole(userId, roleId);

            if (userRole == null)
                return ResultTypes.Failed;


            _role.RemoveUserRole(userRole);

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }

    public bool RoleCheck(Guid userId, List<string> roles)
    {
        try
        {
            var userRoles = _role.GetRolesIdOfUser(userId);

            if (!userRoles.Any())
                return false;

            List<Guid> rolesId = new List<Guid>();


            foreach (var role in roles)
            {
                var roleId = _role.GetRoleIdByName(role.Fixed());

                if (roleId.IsEmpty())
                    continue;


                rolesId.Add(roleId);
            }


            return rolesId.Any(c => userRoles.Contains(c));
        }
        catch
        {
            return false;
        }
    }
}