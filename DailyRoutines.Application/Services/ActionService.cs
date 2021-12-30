using DailyRoutines.Application.Enums;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs;
using DailyRoutines.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = DailyRoutines.Domain.Entities.Action;

namespace DailyRoutines.Application.Services
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _action;

        public ActionService(IActionRepository action)
        {
            _action = action;
        }


        public async Task<Tuple<IEnumerable<ActionList>, int>> GetActionsAsync(Guid userCategoryId, DateTime date, int pageId = 1, int take = 10, string filter = null)
        {
            int skip = (pageId - 1) * take;


            int actionsCount = await _action.GetActionsCount(userCategoryId, date);

            IEnumerable<ActionList> actions = _action.GetActions(userCategoryId, date, skip, take, filter);

            int totalPages = (int)Math.Ceiling(1.0 * actionsCount / take);

            return new Tuple<IEnumerable<ActionList>, int>(actions, totalPages);
        }

        public async Task<ResultTypes> AddActionAsync(Action action)
        {
            try
            {
                await _action.AddActionAsync(action);
                await _action.SaveChangesAsync();

                return ResultTypes.Successful;
            }
            catch
            {
                return ResultTypes.Failed;
            }
        }

        public async Task<ResultTypes> EditActionAsync(Action action)
        {
            try
            {
                _action.UpdateAction(action);
                await _action.SaveChangesAsync();

                return ResultTypes.Successful;
            }
            catch
            {
                return ResultTypes.Failed;
            }
        }

        public async Task<ResultTypes> DeleteActionAsync(Action action)
        {
            try
            {
                _action.RemoveAction(action);
                await _action.SaveChangesAsync();

                return ResultTypes.Successful;
            }
            catch
            {
                return ResultTypes.Failed;
            }
        }

        public async Task<Action> GetActionByIdAsync(Guid actionId) => 
            await _action.GetActionByIdAsync(actionId);

        public async Task<bool> IsUserActionExistAsync(Guid userId, Guid actionId) =>
            await _action.IsUserActionExistAsync(userId, actionId);
    }
}