using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Entities.Models;
using Zust.Helpers;

namespace Zust.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet($"{Constants.Account}/{Constants.ForgotPassword}")]
        public IActionResult ForgotPassword()
        {
            return View(Constants.ForgotPassword);
        }
    }
}
