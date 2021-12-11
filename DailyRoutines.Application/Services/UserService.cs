using System;
using System.Threading.Tasks;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Interfaces;

namespace DailyRoutines.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId) =>
            await _userRepository.GetUserDashboardAsync(userId);
    }
}