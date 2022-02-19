using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Generator;
using DailyRoutines.Domain.DTOs.Routine;

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
        _context.Users.IgnoreQueryFilters()
            .SingleOrDefault(c=> c.Id == userId);

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

    public FilterUsersDTO GetUsers(FilterUsersDTO filter)
    {
        IQueryable<User> users = _context.Users;


        switch (filter.Type)
        {
            case "all":
                {
                    users = users.IgnoreQueryFilters();

                    filter.Type = "all";

                    break;
                }
            case "active":
                {
                    filter.Type = "active";

                    break;
                }
            case "blocked":
                {
                    users = users.Where(c => c.IsBlock)
                        .IgnoreQueryFilters();

                    filter.Type = "all";

                    break;
                }
            default:
            {
                filter.Type = "active";

                break;
            }
        }


        if (!string.IsNullOrEmpty(filter.Search))
            users = users.Where(c => c.FirstName.Contains(filter.Search) ||
                                     c.LastName.Contains(filter.Search) ||
                                     c.PhoneNumber.Contains(filter.Search) ||
                                     c.Email.Contains(filter.Search));

        int pagesCount = (int)Math.Ceiling(users.Count() / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var categories = users
            .Select(c => new UsersListDTO(c.Id, c.FullName, c.PhoneNumber, c.Email,c.IsBlock))
            .Paging(pager).ToList();

        return filter.SetItems(categories)
            .SetPaging(pager);
    }

    public void SaveChanges() =>
        _context.SaveChanges();
}