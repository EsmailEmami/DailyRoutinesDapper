using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DailyRoutines.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DailyRoutinesDbConnection"));
    }


    public void AddUser(User user)
    {
        string query =
            "INSERT INTO [User].[Users] ([FirstName],[LastName],[PhoneNumber],[Email],[Password],[IsBlock])" +
            "OUTPUT CAST([Inserted].[UserId] AS UNIQUEIDENTIFIER) AS [UserId]" +
            "VALUES (@FirstName,@LastName,@PhoneNumber,@Email,@Password,@IsBlock);";

        var userId = _db.QuerySingleOrDefault<Guid>(query, new
        {
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email,
            user.Password,
            user.IsBlock
        });
    }

    public void UpdateUser(User user)
    {
        string query =
            "UPDATE [User].[Users] SET" +
            "[FirstName] = @FirstName, " +
            "[LastName] = @LastName, " +
            "[PhoneNumber] = @PhoneNumber, " +
            "[Email] = @Email, " +
            "[Password] = @Password, " +
            "[LastUpdateDate] = @LastUpdateDate, " +
            "[IsBlock] = @IsBlock " +
            "WHERE [UserId] = @UserId;";


        _db.Execute(query, new
        {
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email,
            user.Password,
            @LastUpdateDate = DateTime.Now,
            user.IsBlock,
            user.UserId
        });
    }

    public bool IsUserExist(string email, string password)
    {
        string query = "SELECT (CASE WHEN EXISTS(" +
                       "SELECT NULL " +
                       "FROM [User].[Users] " +
                       "WHERE ([Email] = @Email) AND ([Password] = @Password)) " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingleOrDefault<bool>(query, new
        {
            email,
            password
        });
    }

    public bool IsUserExist(Guid userId)
    {
        string query = "SELECT (CASE WHEN EXISTS( " +
                       "SELECT NULL " +
                       "FROM [User].[Users] " +
                       "WHERE [UserId] = @UserId) " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingleOrDefault<bool>(query, new
        {
            userId
        });
    }

    public User GetUserByEmail(string email)
    {
        string query =
            "SELECT [UserId],[FirstName],[LastName],[PhoneNumber],[Email]," +
            "[Password],[CreateDate],[LastUpdateDate],[IsBlock] " +
            "FROM [User].[Users] " +
            "WHERE [Email] = @Email;";

        return _db.QuerySingleOrDefault<User>(query, new
        {
            email
        });
    }

    public User GetUserById(Guid userId)
    {
        string query =
            "SELECT [UserId],[FirstName],[LastName],[PhoneNumber],[Email]," +
            "[Password],[CreateDate],[LastUpdateDate],[IsBlock] " +
            "FROM [User].[Users] " +
            "WHERE [UserId] = @UserId;";

        return _db.QuerySingleOrDefault<User>(query, new
        {
            userId
        });
    }

    public bool IsUserPhoneNumberExists(string phoneNumber)
    {
        string query = "SELECT (CASE WHEN EXISTS( " +
                       "SELECT NULL " +
                       "FROM [User].[Users] " +
                       "WHERE [PhoneNumber] = @PhoneNumber " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingleOrDefault<bool>(query, new
        {
            phoneNumber
        });
    }

    public bool IsUserEmailExists(string email)
    {
        string query = "SELECT (CASE WHEN EXISTS( " +
                       "SELECT NULL " +
                       "FROM [User].[Users] " +
                       "WHERE [Email] = @Email " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingleOrDefault<bool>(query, new
        {
            email
        });
    }

    public int GetUsersCount(string type, string filter)
    {
        string query = "SELECT COUNT(*) FROM [User].[Users] ";

        switch (type)
        {
            case "active":
                {
                    query += "WHERE ([IsBlock] = 0)";

                    break;
                }
            case "blocked":
                {
                    query += "WHERE ([IsBlock] = 1)";

                    break;
                }
            case "all":
                {
                    break;
                }
        }

        if (string.IsNullOrEmpty(filter))
        {
            if (type == "all")
            {
                query += " WHERE ";
            }
            else
            {
                query += " AND ";
            }


            query += "([FirstName] LIKE N'%@Search%') OR " +
                     "([LastName] LIKE N'%@Search%') OR " +
                     "([PhoneNumber] LIKE N'%@Search%') OR " +
                     "([Email] LIKE N'%@Search%')";
        }

        return _db.QuerySingleOrDefault<int>(query, new
        {
            @search = filter
        });
    }

    public UserDashboardDTO GetUserDashboard(Guid userId)
    {
        string query =
            "SELECT [FirstName],[LastName],[PhoneNumber],[Email] " +
            "FROM [User].[Users] WHERE [UserId] = @UserId;";

        return _db.QuerySingleOrDefault<UserDashboardDTO>(query, new
        {
            userId
        });
    }

    public EditUserDTO GetUserForEdit(Guid userId)
    {
        string query =
            "SELECT [UserId],[FirstName],[LastName],[PhoneNumber],[Email] " +
            "FROM [User].[Users] " +
            "WHERE [Users].[UserId] = @UserId " +
            "SELECT [RoleId] " +
            "FROM  [Access].[UserRoles] " +
            "WHERE [UserId] = @UserId";


        using var list = _db.QueryMultiple(query, new {userId});

        var data = list.ReadFirstOrDefault<EditUserDTO>();
        data.Roles = list.Read<Guid>().ToList();

        return data;
    }

    public UserInformationDTO GetUserInformation(Guid userId)
    {
        string query =
            "SELECT [UserId],[FirstName],[LastName],[PhoneNumber],[Email]," +
            "[CreateDate],[IsBlock] " +
            "FROM [User].[Users] " +
            "WHERE [UserId] = @UserId;";

        return _db.QuerySingleOrDefault<UserInformationDTO>(query, new
        {
            userId
        });
    }

    public List<UsersListDTO> GetUsers(int skip, int take, string type, string filter)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Skip", skip);
        parameters.Add("@Take", take);
        parameters.Add("@Search", filter);
        parameters.Add("@Type", type);

        return _db.Query<UsersListDTO>("[User].[uspGetUsers]", parameters,
            commandType: CommandType.StoredProcedure).ToList();
    }
}