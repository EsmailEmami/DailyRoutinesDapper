using DailyRoutines.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = DailyRoutines.Domain.Entities.Action;

namespace DailyRoutines.Domain.Interfaces
{
    public interface IActionRepository
    {
        IEnumerable<ActionList> GetActions(Guid userCategoryId, DateTime date, int skip, int take, string filter);
        Task<int> GetActionsCount(Guid userCategoryId, DateTime date);

        Task AddActionAsync(Action action);
        void UpdateAction(Action action);
        void RemoveAction(Action action);

        Task<Action> GetActionByIdAsync(Guid actionId);

        Task<bool> IsUserActionExistAsync(Guid userId, Guid actionId);

        Task SaveChangesAsync();
    }
}