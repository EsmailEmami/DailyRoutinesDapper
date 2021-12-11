using DailyRoutines.Domain.DTOs.User;
using System;
using System.Threading.Tasks;

namespace DailyRoutines.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId);
    }
}