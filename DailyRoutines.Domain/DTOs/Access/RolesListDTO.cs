using System;

namespace DailyRoutines.Domain.DTOs.Access;

public class RolesListDTO
{
    public RolesListDTO(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }

    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}