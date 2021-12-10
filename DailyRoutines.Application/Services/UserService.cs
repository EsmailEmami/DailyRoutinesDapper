using DailyRoutines.Application.Interfaces;
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
    }
}