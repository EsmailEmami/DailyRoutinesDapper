using System;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Enums;
using DailyRoutines.Domain.Interfaces;

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
            _role.saveChanges();

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
            _role.updateRole(role);
            _role.saveChanges();

            return ResultTypes.Successful;
        }
        catch
        {
            return ResultTypes.Failed;
        }
    }
}