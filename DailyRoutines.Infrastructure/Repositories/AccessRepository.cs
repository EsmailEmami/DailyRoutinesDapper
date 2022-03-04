using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.Common;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DailyRoutines.Infrastructure.Repositories;

public class AccessRepository : IAccessRepository
{
    private readonly IDbConnection _db;

    public AccessRepository(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DailyRoutinesDbConnection"));
    }

    public List<UsersListDTO> GetUsersWithRole(int skip, int take, string type, string filter)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);
        parameters.Add("@Type", type);

        return _db.Query<UsersListDTO>("[User].[uspGetUsersWithRoles]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }

    public List<RolesListDTO> GetRoles(int skip, int take, string filter)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);

        return _db.Query<RolesListDTO>("[Access].[uspGetRoles]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }

    public List<RolesListDTO> GetUserRoles(Guid userId, int skip, int take, string filter)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);

        return _db.Query<RolesListDTO>("[Access].[uspGetUserRoles]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }

    public EditRoleDTO GetRoleForEdit(Guid roleId)
    {
        string query = "SELECT [RoleId],[RoleName] " +
                       "FROM [Access].[Roles] " +
                       "WHERE [RoleId] = @RoleId";

        return _db.QuerySingleOrDefault<EditRoleDTO>(query, new
        {
            roleId
        });
    }

    public Role GetRoleById(Guid roleId)
    {
        string query = "SELECT [RoleId],[RoleName] " +
                       "FROM [Access].[Roles] " +
                       "WHERE [RoleId] = @RoleId";

        return _db.QuerySingleOrDefault<Role>(query, new
        {
            roleId
        });
    }

    public void AddRole(Role role)
    {
        string query = "INSERT INTO [Access].[Roles] ([RoleName]) " +
                       "OUTPUT [Inserted].[RoleId] " +
                       "VALUES (@RoleName)";

        var roleId = _db.QuerySingleOrDefault<Guid>(query, new
        {
            role.RoleName
        });
    }

    public void UpdateRole(Role role)
    {
        string query = "UPDATE [Access].[Roles] SET " +
                       "[RoleName] = @RoleName " +
                       "WHERE [RoleId] = @RoleId";

        _db.Execute(query, new
        {
            role.RoleName,
            role.roleId
        });
    }

    public List<ItemsForSelectDTO> GetRolesForSelect()
    {
        string query =
            "SELECT [RoleId] AS [Value], [RoleName] AS [Name] " +
            "FROM [Access].[Roles]";

        return _db.Query<ItemsForSelectDTO>(query).ToList();
    }

    public Guid GetRoleIdByName(string roleName)
    {
        string query = "SELECT [RoleId] " +
                       "FROM [Access].[Roles] " +
                       "WHERE [RoleName] = @RoleName";

        return _db.QuerySingleOrDefault<Guid>(query, new
        {
            roleName
        });
    }

    public int GetRolesCount(string filter)
    {
        string query = "SELECT COUNT(*) " +
                       "FROM [Access].[Roles]";

        if (!string.IsNullOrEmpty(filter))
        {
            query += " WHERE [RoleName] LIKE %@Search%";
        }

        return _db.QuerySingleOrDefault<int>(query, new
        {
            @Search = filter
        });
    }

    public int GetUserRolesCount(Guid userId, string filter)
    {
        string query = "SELECT COUNT(*) " +
                       "FROM [Access].[UserRoles] " +
                       "INNER JOIN [Access].[Roles] " +
                       "ON [Access].[UserRoles].[RoleId] = [Access].[Roles].[RoleId] " +
                       "WHERE ([UserRoles].[UserId] = @UserId)";

        if (!string.IsNullOrEmpty(filter))
        {
            query += " AND ([Roles].[RoleName] LIKE %@Search%)";
        }

        return _db.QuerySingleOrDefault<int>(query, new
        {
            userId,
            @Search = filter
        });
    }

    public int GetUsersWithRoleCount(string filter)
    {
        string query = "SELECT COUNT(*) " +
                       "FROM [User].[Users] " +
                       "INNER JOIN [Access].[UserRoles] " +
                       "ON [User].[Users].[UserId] = [Access].[UserRoles].[UserId]";

        if (!string.IsNullOrEmpty(filter))
        {
            query += " WHERE ([Users].[FirstName] LIKE N'@Search%') OR " +
                     "([Users].[LastName] LIKE N'@Search%') OR " +
                     "([Users].[PhoneNumber] LIKE N'@Search%') OR " +
                     "([Users].[Email] LIKE N'@Search%')";
        }

        return _db.QuerySingleOrDefault<int>(query, new
        {
            @Search = filter
        });
    }

    public List<UserRole> GetUserRoles(Guid userId)
    {
        string query = "SELECT [UserId],[RoleId] " +
                       "FROM [Access].[UserRoles] " +
                       "WHERE[UserId] = @UserId";

        return _db.Query<UserRole>(query, new
        {
            userId
        }).ToList();
    }

    public void RemoveUserRole(UserRole userRole)
    {
        string query = "DELETE [Access].[UserRoles] " +
                       "WHERE ([UserId] = @UserId) AND ([RoleId] = @RoleId)";

        _db.Execute(query, new
        {
            userRole.UserId,
            userRole.RoleId
        });
    }

    public void AddUserRole(UserRole userRole)
    {
        string query = "INSERT INTO [Access].[UserRoles] ([UserId],[RoleId])" +
                       "VALUES (@UserId,@RoleId)";

        _db.Execute(query, new
        {
            userRole.UserId,
            userRole.RoleId
        });
    }

    public UserRole GetUserRole(Guid userId, Guid roleId)
    {
        string query = "SELECT [UserId],[RoleId]" +
                       "FROM [Access].[UserRoles] " +
                       "WHERE ([UserId] = @UserId) AND ([RoleId] = @RoleId)";

        return _db.QuerySingleOrDefault<UserRole>(query, new
        {
            userId,
            roleId
        });
    }

    public List<Guid> GetRolesIdOfUser(Guid userId)
    {
        string query = "SELECT [RoleId]" +
                       "FROM [Access].[UserRoles] " +
                       "WHERE [UserId] = @UserId";

        return _db.Query<Guid>(query, new
        {
            userId
        }).ToList();
    }
}