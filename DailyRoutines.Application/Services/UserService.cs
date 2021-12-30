using System;
using System.Threading.Tasks;
using DailyRoutines.Application.Enums;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities;
using DailyRoutines.Domain.Interfaces;

namespace DailyRoutines.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;

        public UserService(IUserRepository user)
        {
            _user = user;
        }

        public async Task<ResultTypes> AddUserAsync(User user)
        {
            try
            {
                await _user.AddUserAsync(user);
                await _user.SaveChangesAsync();

                return ResultTypes.Successful;
            }
            catch
            {
                return ResultTypes.Failed;
            }
        }

        public async Task<bool> IsUserExistAsync(string email, string password)
        {
            string encodedPass = PasswordHelper.EncodePasswordMd5(password);

            return await _user.IsUserExistAsync(email, password);
        }

        public async Task<User> GetUserByEmailAsync(string email) =>
            await _user.GetUserByEmailAsync(email);

        public async Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId) =>
            await _user.GetUserDashboardAsync(userId);
    }
}