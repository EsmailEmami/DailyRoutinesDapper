using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;

namespace DailyRoutines.Domain.Interfaces;

public interface IAccessRepository
{
    List<UsersListDTO> GetUsersWithRole(int skip, int take, string type, string filter);
    List<RolesListDTO> GetRoles(int skip, int take, string filter);
    List<RolesListDTO> GetUserRoles(Guid userId, int skip, int take, string filter);

    EditRoleDTO GetRoleForEdit(Guid roleId);
    Role GetRoleById(Guid roleId);
    void AddRole(Role role);
    void UpdateRole(Role role);
    List<ItemsForSelectDTO> GetRolesForSelect();
    Guid GetRoleIdByName(string roleName);

    int GetRolesCount(string filter);
    int GetUserRolesCount(Guid userId, string filter);
    int GetUsersWithRoleCount(string filter);

    #region user role

    List<UserRole> GetUserRoles(Guid userId);
    void RemoveUserRole(UserRole userRole);
    void AddUserRole(UserRole userRole);
    UserRole GetUserRole(Guid userId, Guid roleId);

    List<Guid> GetRolesIdOfUser(Guid userId);


    #endregion
}
