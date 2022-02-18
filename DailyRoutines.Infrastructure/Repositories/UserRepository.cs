using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRoutines.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DailyRoutinesDbContext _context;

    public UserRepository(DailyRoutinesDbContext context)
    {
        _context = context;
    }

    public void AddUser(User user) => _context.Users.Add(user);

    public void UpdateUser(User user) =>
        _context.Users.Update(user);

    public void RemoveUser(User user) =>
        _context.Users.Remove(user);

    public bool IsUserExist(string email, string password) =>
        _context.Users.Any(c => c.Email == email && c.Password == password);

    public User GetUserByEmail(string email) =>
        _context.Users.SingleOrDefault(c => c.Email == email);

    public User GetUserById(Guid userId) =>
        _context.Users.Find(userId);

    public bool IsUserPhoneNumberExists(string phoneNumber) =>
        _context.Users.Any(c => c.PhoneNumber == phoneNumber);

    public bool IsUserEmailExists(string email) =>
        _context.Users.Any(c => c.Email == email);

    public UserDashboardDTO GetUserDashboard(Guid userId) =>
        _context.Users.Where(c => c.Id == userId)
            .Select(c => new UserDashboardDTO()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
            }).SingleOrDefault();

    public void SaveChanges() =>
        _context.SaveChanges();
}