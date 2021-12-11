using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities;
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

        public async Task AddUserAsync(User user) =>
            await _context.Users.AddAsync(user);

        public void UpdateUser(User user) =>
            _context.Users.Update(user);

        public void RemoveUser(User user) =>
            _context.Users.Remove(user);

        public async Task<bool> IsUserExistAsync(string email, string password) =>
            await _context.Users.AnyAsync(c => c.Email == email && c.Password == password);

        public async Task<User> GetUserByEmailAsync(string email) =>
            await _context.Users.SingleOrDefaultAsync(c => c.Email == email);

        public async Task<UserDashboardForShow> GetUserDashboardAsync(Guid userId) =>
            await _context.Users.Where(c => c.UserId == userId)
                .Select(c => new UserDashboardForShow()
                {
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                }).SingleOrDefaultAsync();

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}