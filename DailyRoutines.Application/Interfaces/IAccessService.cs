using System;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Enums;

namespace DailyRoutines.Application.Interfaces;

public interface IAccessService
{
    FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter);
    FilterRolesDTO GetRoles(FilterRolesDTO filter);

    EditRoleDTO GetRoleForEdit(Guid roleId);
    Role GetRoleById(Guid roleId);

    ResultTypes AddRole(Role role);
    ResultTypes EditRole(Role role);
}