using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyRoutines.web.Pages.Action
{
    public class ActionListModel : PageModel
    {
        private readonly IActionService _actionService;

        public ActionListModel(IActionService actionService)
        {
            _actionService = actionService;
        }

        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<ActionList> Actions { get; set; }

        public async Task OnGet(Guid userCategoryId, int pageId, int year, int month, int day)
        {
            PageId = pageId;

            DateTime requestDate = new DateTime(year, month, day);

            var (actions, totalPages) = await _actionService.GetActionsAsync(userCategoryId, requestDate, pageId, 10);

            Actions = actions;
            TotalPages = totalPages;
        }
    }
}
