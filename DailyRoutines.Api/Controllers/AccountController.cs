using DailyRoutines.Application.Common;
using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Interfaces;
using DailyRoutines.Application.Security;
using DailyRoutines.Domain.DTOs.Routine;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DailyRoutines.Api.Controllers;

public class AccountController : SiteBaseController
{
    #region constructor

    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IRoutineService _routineService;

    public AccountController(IUserService userService, IConfiguration configuration, IRoutineService routineService)
    {
        _userService = userService;
        _configuration = configuration;
        _routineService = routineService;
    }

    #endregion

    #region Register

    [HttpPost("[action]")]
    public IActionResult Register([FromBody] RegisterDTO registerData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error(new
            {
                message = "اطلاعات وارد شده نادرست است."
            });

        if (_userService.IsUserEmailExists(registerData.Email))
            return JsonResponseStatus.Error(new
            {
                message = "ایمیل وارد شده توسط شخص دیگری ثبت شده است."
            });

        var user = new User()
        {
            CreateDate = DateTime.Now,
            Email = registerData.Email.Fixed(),
            FirstName = registerData.FirstName,
            LastName = registerData.LastName,
            Password = registerData.Password
        };

        var res = _userService.AddUser(user);

        if (res == ResultTypes.Successful)
        {

            //var mailBody = _viewRenderService.RenderToString("Email/ActivateAccount", null).Result;


            // _messageSender.SendEmail("esmaeilemami84@gmail.com", "ایمیل فعال سازی", mailBody, true);


            return JsonResponseStatus.Success();
        }

        return JsonResponseStatus.Error(new
        {
            message = "مشکلی پیش آمده است. لطفا دوباره تلاش کنید."
        });
    }

    #endregion

    #region Login

    [HttpPost("[action]")]
    public IActionResult Login([FromBody] LoginDTO loginData)
    {
        if (!ModelState.IsValid)
            return JsonResponseStatus.Error(new
            {
                message = "اطلاعات وارد شده نادرست است."
            });

        if (!_userService.IsUserExist(loginData.Email, loginData.Password))
            return JsonResponseStatus.Error(new
            {
                message = "کاربری با مشخصات وارد شده یافت نشد."
            });


        var user = _userService.GetUserByEmail(loginData.Email);

        if (user == null)
            return JsonResponseStatus.Error(new
            {
                message = "مشکلی پیش آمده است. لطفا دوباره تلاش کنید."
            });



        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            claims: new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            },
            expires: DateTime.Now.AddDays(30),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return JsonResponseStatus.Success(new LoginUser(
            user.Id,
            user.FirstName,
            user.LastName,
            30,
            tokenString));

    }

    #endregion

    #region Check User Authentication

    [Access]
    [HttpPost("[action]")]
    public IActionResult CheckUserAuthentication()
    {
        var user = _userService.GetUserById(User.GetUserId());

        return JsonResponseStatus.Success(new
        {
            userId = user.Id,
            firstName = user.FirstName,
            lastName = user.LastName,
            email = user.Email,
        });
    }

    #endregion

    [HttpGet("[action]")]
    public IActionResult Test([FromQuery] Guid userId, [FromQuery] Guid categoryId, [FromQuery] int year)
    {
        var b = _routineService.GetActionsMonthOfCategory(categoryId, year);

        return null;
    }
}