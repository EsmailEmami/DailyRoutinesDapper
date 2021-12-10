using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;

namespace DailyRoutines.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DailyRoutinesDbContext _context;

        public UserRepository(DailyRoutinesDbContext context)
        {
            _context = context;
        }
    }
}