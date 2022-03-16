using DailyRoutines.Application.Security;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DailyRoutines.Api.Hubs;

[Access]
public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        //var userId = Context.User.GetUserId();

        return base.OnConnectedAsync();
    }

    public Task SendMessageToUser(Guid userId, string message)
    {
        Clients.Caller.SendAsync("ReceiveCallerMessage", message);

        Clients.User(userId.ToString()).SendAsync("ReceiveUserMessage", message);

        return Task.CompletedTask;
    }
}