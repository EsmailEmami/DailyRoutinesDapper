using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;

namespace DailyRoutines.Infrastructure.Repositories;

public class AccessRepository : IAccessRepository
{
    private readonly DailyRoutinesDbContext _context;

    public AccessRepository(DailyRoutinesDbContext context)
    {
        _context = context;
    }
}