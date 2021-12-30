using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;

namespace DailyRoutines.Infrastructure.Repositories
{
    public class UserCategoryRepository : IUserCategoryRepository
    {
        private readonly DailyRoutinesDbContext _context;

        public UserCategoryRepository(DailyRoutinesDbContext context)
        {
            _context = context;
        }
    }
}