using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRoutines.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DailyRoutinesDbContext _context;

        public UserRepository(DailyRoutinesDbContext context)
        {
            _context = context;
        }

        public async Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId) =>
            await _context.Users.Where(c => c.UserId == userId)
                .Select(c => new UserDashboardForShow()
                {
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                }).SingleOrDefaultAsync();
    }
}