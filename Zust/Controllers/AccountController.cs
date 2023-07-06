using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Entities.Models;
using Zust.Web.Helpers.Constants;
using Zust.Web.Models;

namespace Zust.Web.Controllers
{
    [Controller]
    public class AccountController : Controller
    {
        /// <summary>
        /// Displays the login view.
        /// </summary>
        /// <returns>The login view.</returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Displays the register view with an empty register model.
        /// </summary>
        /// <returns>The register view.</returns>
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        /// <summary>
        /// Displays the settings view.
        /// </summary>  
        /// <returns>The settings view.</returns>
        public IActionResult Settings()
        {
            return View();
        }

        /// <summary>
        /// Displays the forgot password view.
        /// </summary>
        /// <returns>The forgot password view.</returns>
        [HttpGet($"{UrlConstants.Account}/{UrlConstants.ForgotPassword}")]
        public IActionResult ForgotPassword()
        {
            return View(UrlConstants.ForgotPassword);
        }
    }
}
