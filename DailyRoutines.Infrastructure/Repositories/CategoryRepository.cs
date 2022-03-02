using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;

namespace DailyRoutines.Infrastructure.Repositories;

public class CategoryRepository : IUserCategoryRepository
{
    private readonly DailyRoutinesDbContext _context;

    public CategoryRepository(DailyRoutinesDbContext context)
    {
        _context = context;
    }
}