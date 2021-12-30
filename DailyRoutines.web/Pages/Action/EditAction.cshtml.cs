using DailyRoutines.Application.Convertors;
using DailyRoutines.Application.Enums;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DailyRoutines.web.Pages.Action
{
    public class EditActionModel : PageModel
    {
        private readonly IActionService _actionService;

        public EditActionModel(IActionService actionService)
        {
            _actionService = actionService;
        }


        [BindProperty]
        public Domain.Entities.Action Action { get; set; }

        public async Task<IActionResult> OnGet(Guid actionId)
        {
            if (actionId.IsEmpty())
                return new JsonResult(new { isValid = false, alertType = "danger", alertText = "متاسقانه مشکلی پیش آمده است لطفا دوباره تلاش کنید." });

            Guid userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToGuid();

            if (!await _actionService.IsUserActionExistAsync(userId, actionId))
                return new JsonResult(new { isValid = false, alertType = "danger", alertText = "متاسقانه مشکلی پیش آمده است لطفا دوباره تلاش کنید." });

            Action = await _actionService.GetActionByIdAsync(actionId);

            return Page();
        }

        public async Task<JsonResult> OnPost()
        {
            if (!ModelState.IsValid)
                return new JsonResult(new { isValid = false, html = "" });

            var addAction = await _actionService.EditActionAsync(Action);

            if (addAction != ResultTypes.Successful)
            {
                ModelState.AddModelError("", "متاسفانه هنگام ویرایش فعالیت به مشکلی غیر منتظره برخوردیم.");

                var currentView = await this.RenderViewAsync("EditAction");

                return new JsonResult(new { isValid = false, html = currentView });
            }

            return new JsonResult(new
            {
                isValid = true,
                alertType = "success",
                alertText = "فعالیت شما با موفقیت ویرایش شد",
                replaceMain = ""
            });
        }
    }
}
