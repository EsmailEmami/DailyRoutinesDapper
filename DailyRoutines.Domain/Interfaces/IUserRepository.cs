using DailyRoutines.Domain.DTOs.User;
using System;
using System.Threading.Tasks;
using DailyRoutines.Domain.Entities;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);

        Task<bool> IsUserExistAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);

        Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId);

        Task SaveChangesAsync();
    }
}