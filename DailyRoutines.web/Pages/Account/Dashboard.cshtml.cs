using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DailyRoutines.web.Pages.Account
{
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;

        public DashboardModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserDashboardForShow Dashboard { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Guid userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToGuid();

            if (userId.IsEmpty())
                return NotFound();

            var data = await _userService.GetUserDashboardAsync(userId);

            if (data == null)
                return NotFound();

            Dashboard = data;

            return Page();
        }
    }
}
