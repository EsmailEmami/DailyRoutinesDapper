using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;

namespace DailyRoutines.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DailyRoutinesDbContext _context;

        public RoleRepository(DailyRoutinesDbContext context)
        {
            _context = context;
        }
    }
}