using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.Interfaces;

namespace DailyRoutines.Application.Services
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;

        public ActionService(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }
    }
}