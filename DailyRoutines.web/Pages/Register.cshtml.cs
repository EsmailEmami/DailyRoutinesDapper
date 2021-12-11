using DailyRoutines.Application.Enums;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace DailyRoutines.web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public RegisterViewModel Register { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var user = new User
            {
                FirstName = Register.FirstName,
                LastName = Register.LastName,
                Email = Register.Email,
                PhoneNumber = Register.PhoneNumber,
                Password = PasswordHelper.EncodePasswordMd5(Register.Password)
            };
            var addUser = await _userService.AddUserAsync(user);

            if (addUser == ResultTypes.Successful)
            {
                return RedirectToPage("/");
            }
            else
            {
                ModelState.AddModelError("", "متاسفانه هنگام ثبت نام به مشکلی غیر منتظره برخوردیم.");
                return Page();
            }
        }
    }
}