using DailyRoutines.Application.Enums;
using DailyRoutines.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = DailyRoutines.Domain.Entities.Action;

namespace DailyRoutines.Application.Interfaces
{
    public interface IActionService
    {
        Task<Tuple<IEnumerable<ActionList>, int>> GetActionsAsync(Guid userCategoryId, DateTime date, int pageId = 1, int take = 10, string filter = null);
        Task<ResultTypes> AddActionAsync(Action action);
        Task<ResultTypes> EditActionAsync(Action action);
        Task<ResultTypes> DeleteActionAsync(Action action);

        Task<Action> GetActionByIdAsync(Guid actionId);

        Task<bool> IsUserActionExistAsync(Guid userId, Guid actionId);
    }
}