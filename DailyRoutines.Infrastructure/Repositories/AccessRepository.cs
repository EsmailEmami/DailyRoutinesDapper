using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace DailyRoutines.Infrastructure.Repositories;

public class AccessRepository : IAccessRepository
{
    public FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter)
    {
        throw new NotImplementedException();
    }

    public FilterRolesDTO GetRoles(FilterRolesDTO filter)
    {
        throw new NotImplementedException();
    }

    public FilterRolesDTO GetUserRoles(Guid userId, FilterRolesDTO filter)
    {
        throw new NotImplementedException();
    }

    public EditRoleDTO GetRoleForEdit(Guid roleId)
    {
        throw new NotImplementedException();
    }

    public Role GetRoleById(Guid roleId)
    {
        throw new NotImplementedException();
    }

    public void AddRole(Role role)
    {
        throw new NotImplementedException();
    }

    public void UpdateRole(Role role)
    {
        throw new NotImplementedException();
    }

    public List<ItemsForSelectDTO> GetRolesForSelect()
    {
        throw new NotImplementedException();
    }

    public Guid GetRoleIdByName(string roleName)
    {
        throw new NotImplementedException();
    }

    public List<UserRole> GetUserRoles(Guid userId)
    {
        throw new NotImplementedException();
    }

    public void RemoveUserRole(UserRole userRole)
    {
        throw new NotImplementedException();
    }

    public void AddUserRole(UserRole userRole)
    {
        throw new NotImplementedException();
    }

    public UserRole GetUserRole(Guid userId, Guid roleId)
    {
        throw new NotImplementedException();
    }

    public List<Guid> GetRolesIdOfUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}