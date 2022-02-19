using System;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IAccessRepository
    {
        FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter);
        FilterRolesDTO GetRoles(FilterRolesDTO filter);

        EditRoleDTO GetRoleForEdit(Guid roleId);
        Role GetRoleById(Guid roleId);
        void AddRole(Role role);
        void updateRole(Role role);

        void saveChanges();
    }
}