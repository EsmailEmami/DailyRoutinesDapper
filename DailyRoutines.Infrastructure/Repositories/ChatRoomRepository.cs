using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

    public List<MessageForShowDTO> GetUserMessagesHistory(Guid fromUser, Guid toUser)
    {
        string query = "SELECT [MessageId],[Message],[SendAt],IIF([FromUser] = @FromUser, 1, 0)  AS [YouSent] " +
                       "FROM [Chat].[Message] " +
                       "WHERE ([FromUser] IN (@FromUser, @ToUser)) " +
                       "AND ([ToUser] IN (@FromUser, @ToUser)) " +
                       "ORDER BY [MessageId] ASC";
        return _db.Query<MessageForShowDTO>(query, new
        {
            @FromUser = fromUser,
            @ToUser = toUser
        }).ToList();
    }
}