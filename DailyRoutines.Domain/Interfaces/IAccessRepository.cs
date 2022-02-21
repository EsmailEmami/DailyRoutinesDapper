using System;
using System.Collections.Generic;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IAccessRepository
    {
        FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter);
        FilterRolesDTO GetRoles(FilterRolesDTO filter);
        FilterRolesDTO GetUserRoles(Guid userId, FilterRolesDTO filter);

        EditRoleDTO GetRoleForEdit(Guid roleId);
        Role GetRoleById(Guid roleId);
        void AddRole(Role role);
        void UpdateRole(Role role);
        List<ItemsForSelectDTO> GetRolesForSelect();


        #region user role

        List<UserRole> GetUserRoles(Guid userId);
        void RemoveUserRole(UserRole userRole);
        void AddUserRole(UserRole userRole);
        UserRole GetUserRole(Guid userId, Guid roleId);

        #endregion

        void SaveChanges();
    }
}