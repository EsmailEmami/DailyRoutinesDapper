using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DailyRoutines.web.Pages.Action
{
    public class AddActionModel : PageModel
    {


        public async Task<IActionResult> OnGet(Guid userCategoryId)
        {
            return Page();
        }
    }
}
