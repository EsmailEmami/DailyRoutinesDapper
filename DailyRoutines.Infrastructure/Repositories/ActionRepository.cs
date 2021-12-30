using DailyRoutines.Domain.DTOs;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action = DailyRoutines.Domain.Entities.Action;

namespace DailyRoutines.Infrastructure.Repositories
{
    public class ActionRepository : IActionRepository
    {
        private readonly DailyRoutinesDbContext _context;

        public ActionRepository(DailyRoutinesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ActionList> GetActions(Guid userCategoryId, DateTime date, int skip, int take, string filter)
        {
            IQueryable<Action> actions = _context.Actions
                .Where(c => c.UserCategoryId == userCategoryId &&
                            c.CreateDate.Date == date);


            if (!string.IsNullOrEmpty(filter))
            {
                actions = actions.Where(c => c.ActionTitle.Contains(filter) || c.ActionDescription.Contains(filter));
            }

            actions = actions.Skip(skip).Take(take);

            return actions.Select(c => new ActionList(c.ActionId, c.ActionTitle, c.CreateDate));
        }

        public async Task<int> GetActionsCount(Guid userCategoryId, DateTime date) =>
            await _context.Actions
                .CountAsync(c => c.UserCategoryId == userCategoryId &&
                                 c.CreateDate.Date == date);

        public async Task AddActionAsync(Action action) => await _context.Actions.AddAsync(action);

        public void UpdateAction(Action action) => _context.Actions.Update(action);

        public void RemoveAction(Action action) => _context.Actions.Remove(action);
        public async Task<Action> GetActionByIdAsync(Guid actionId) => await _context.Actions.FindAsync(actionId);

        public async Task<bool> IsUserActionExistAsync(Guid userId, Guid actionId) =>
            await _context.Actions.AnyAsync(c => c.ActionId == actionId &&
                                                 c.UserCategory.UserId == userId);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}