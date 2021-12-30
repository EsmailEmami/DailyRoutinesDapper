using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Enums;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace DailyRoutines.web.Pages.Action
{
    public class AddActionModel : PageModel
    {
        private readonly IActionService _actionService;

        public AddActionModel(IActionService actionService)
        {
            _actionService = actionService;
        }


        [BindProperty]
        public Domain.Entities.Action Action { get; set; }

        public IActionResult OnGet(Guid userCategoryId)
        {
            if (userCategoryId.IsEmpty())
                return new JsonResult(new { isValid = false, alertType = "danger", alertText = "متاسقانه مشکلی پیش آمده است لطفا دوباره تلاش کنید." });

            Action.UserCategoryId = userCategoryId;

            return Page();
        }

        public async Task<JsonResult> OnPost()
        {
            if (!ModelState.IsValid)
                return new JsonResult(new { isValid = false, html = "" });

            var addAction = await _actionService.AddActionAsync(Action);

            if (addAction != ResultTypes.Successful)
            {
                ModelState.AddModelError("", "متاسفانه هنگام ثبت فعالیت به مشکلی غیر منتظره برخوردیم.");

                var currentView = await this.RenderViewAsync("AddAction");

                return new JsonResult(new { isValid = false, html = currentView });
            }
            
            return new JsonResult(new
            {
                isValid = true,
                alertType = "success",
                alertText = "فعالیت شما با موفقیت ثبت شد",
                replaceMain = ""
            });
        }
    }
}
