using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Interfaces;
using System;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DailyRoutines.Infrastructure.Repositories;

public class UserRepositoryDapper : IUserRepository
{
    private IDbConnection _db;

    public UserRepositoryDapper(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DailyRoutinesDbConnection"));
    }


    public void AddUser(User user)
    {
        string query =
            "INSERT INTO [User].[Users] ([FirstName],[LastName],[PhoneNumber],[Email],[Password],[IsBlock])" +
            "OUTPUT CAST([Inserted].[UserId] AS UNIQUEIDENTIFIER) AS [UserId]" +
            "VALUES (@FirstName,@LastName,@PhoneNumber,@Email,@Password,@IsBlock);";

        Guid userId = _db.Query<Guid>(query, new
        {
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email,
            user.Password,
            user.IsBlock
        }).SingleOrDefault();
    }

    public void UpdateUser(User user)
    {
        string query =
            "UPDATE [User].[Users] SET" +
            "[FirstName] = @FirstName, " +
            "[LastName] = @LastName, " +
            "[PhoneNumber] = @PhoneNumber, " +
            "[Email] = @Email, " +
            "[Password] = @Password" +
            "[LastUpdateDate] = @LastUpdateDate, " +
            "[IsBlock] = @IsBlock" +
            "WHERE [UserId] = @UserId;";


        _db.Execute(query, new
        {
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email,
            user.Password, 
            DateTime.Now,
            user.IsBlock,
            user.Id
        });
    }

    public void RemoveUser(User user)
    {
        string query = "DELETE FROM [User].[Users]" +
                       "WHERE [UserId] = @UserId;";


        _db.Execute(query, new
        {
            user.Id
        });
    }

    public bool IsUserExist(string email, string password)
    {
        string query = "SELECT (CASE WHEN EXISTS( " +
                       "SELECT NULL " +
                       "FROM [User].[Users] " +
                       "WHERE ([Email] = @Email) AND ([Password] = @Password) " +
                       "THEN 1 ELSE 0 END) AS[Value]";

        return _db.QuerySingle<bool>(query, new
        {
            email,
            password
        });
    }

    public bool IsUserExist(Guid userId)
    {
        throw new NotImplementedException();
    }

    public User GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public User GetUserById(Guid userId)
    {
        throw new NotImplementedException();
    }

    public bool IsUserPhoneNumberExists(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public bool IsUserEmailExists(string email)
    {
        throw new NotImplementedException();
    }

    public UserDashboardDTO GetUserDashboard(Guid userId)
    {
        throw new NotImplementedException();
    }

    public EditUserDTO GetUserForEdit(Guid userId)
    {
        throw new NotImplementedException();
    }

    public UserInformationDTO GetUserInformation(Guid userId)
    {
        throw new NotImplementedException();
    }

    public FilterUsersDTO GetUsers(FilterUsersDTO filter)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}