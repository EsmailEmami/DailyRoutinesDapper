using DailyRoutines.Application.Interfaces;
using DailyRoutines.Domain.DTOs.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DailyRoutines.web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public LoginViewModel Login { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            bool isUserExist = await _userService.IsUserExistAsync(Login.Email, Login.Password);

            if (!isUserExist)
            {
                ModelState.AddModelError("", "کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            var user = await _userService.GetUserByEmailAsync(Login.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "متاسقانه مشکلی پیش آمده است لطفا دوباره تلاش کنید");
                return Page();
            }

            // settings of user data for login
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = Login.RememberMe,
            };

            // login user in site
            await HttpContext.SignInAsync(principal, properties);

            if (ReturnUrl != null && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return Redirect("/");
        }
    }
}
