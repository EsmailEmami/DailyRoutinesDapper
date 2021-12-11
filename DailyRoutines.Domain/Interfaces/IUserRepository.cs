using DailyRoutines.Domain.DTOs.User;
using System;
using System.Threading.Tasks;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId);
    }
}