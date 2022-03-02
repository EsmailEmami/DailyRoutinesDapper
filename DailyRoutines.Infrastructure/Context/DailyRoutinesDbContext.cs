﻿using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Entities.Routine;
using DailyRoutines.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace DailyRoutines.Infrastructure.Context;

public class DailyRoutinesDbContext : DbContext
{
    public DailyRoutinesDbContext(DbContextOptions<DailyRoutinesDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> UserCategories { get; set; }
    public DbSet<Action> Actions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>()
            .HasKey(c => new { c.UserId, c.RoleId });

        modelBuilder.Entity<Category>()
            .HasQueryFilter(u => !u.IsDelete);

        modelBuilder.Entity<Action>()
            .HasQueryFilter(u => !u.UserCategory.IsDelete);

        modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.IsBlock);

        modelBuilder.Entity<UserRole>()
            .HasQueryFilter(u => !u.User.IsBlock);

        base.OnModelCreating(modelBuilder);
    }
}