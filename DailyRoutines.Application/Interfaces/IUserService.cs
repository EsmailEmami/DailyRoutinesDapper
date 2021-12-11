using DailyRoutines.Application.Enums;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DailyRoutines.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResultTypes> AddUserAsync(User user);
        Task<bool> IsUserExistAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);

        Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId);
    }
}