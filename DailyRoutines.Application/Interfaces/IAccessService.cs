using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Enums;

namespace DailyRoutines.Application.Interfaces;

public interface IAccessService
{
    FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter);
    FilterRolesDTO GetRoles(FilterRolesDTO filter);
    FilterRolesDTO GetUserRoles(Guid userId, FilterRolesDTO filter);

    EditRoleDTO GetRoleForEdit(Guid roleId);
    Role GetRoleById(Guid roleId);

    ResultTypes AddRole(Role role);
    ResultTypes EditRole(Role role);

    List<ItemsForSelectDTO> GetRolesForSelect();

    ResultTypes RemoveAllUserRoles(Guid userId);
    ResultTypes AddUserRole(Guid userId, List<Guid> roles);
    ResultTypes DeleteUserRole(Guid userId, Guid roleId);

    bool RoleCheck(Guid userId, List<string> roles);
}