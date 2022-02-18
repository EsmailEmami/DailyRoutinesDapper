using Microsoft.AspNetCore.Mvc;

namespace DailyRoutines.Api.Controllers;

public class UsersManagerController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}