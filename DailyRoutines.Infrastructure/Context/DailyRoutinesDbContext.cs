using DailyRoutines.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyRoutines.Infrastructure.Context
{
    public class DailyRoutinesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}