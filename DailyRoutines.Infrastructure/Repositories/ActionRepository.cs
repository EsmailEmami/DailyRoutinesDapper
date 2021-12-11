using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;

namespace DailyRoutines.Infrastructure.Repositories
{
    public class ActionRepository :IActionRepository
    {
        private readonly DailyRoutinesDbContext _context;

        public ActionRepository(DailyRoutinesDbContext context)
        {
            _context = context;
        }

    }
}