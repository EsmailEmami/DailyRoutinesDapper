using System.Data;
using System.Data.SqlClient;
using DailyRoutines.Domain.DTOs.Chat;
using DailyRoutines.Domain.Entities.Chat;
using DailyRoutines.Domain.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DailyRoutines.Infrastructure.Repositories;

public class ChatRoomRepository : IChatRoomRepository
{
    private readonly IDbConnection _db;

    public ChatRoomRepository(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DailyRoutinesDbConnection"));
    }

    public MessageForShowDTO AddMessage(ChatMessage message)
    {
        string query = "INSERT INTO [Chat].[Message] ([FromUser],[ToUser],[Message]) " +
                       "VALUES (@FromUser,@ToUser,@Message);";

        return _db.QuerySingleOrDefault<MessageForShowDTO>(query, new
        {
            message.FromUser,
            message.ToUser,
            message.Message
        });
    }
}