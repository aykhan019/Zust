using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using Zust.Models;
using Zust.Web.Helpers.Constants;

namespace Zust.Web.Controllers
{
    [Authorize]
    [Controller]
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays the Birthday view.
        /// </summary>
        /// <returns>The Birthday view.</returns>
        public IActionResult Birthday()
        {
            return View();
        }

        /// <summary>
        /// Displays the Events view.
        /// </summary>
        /// <returns>The Events view.</returns>
        public IActionResult Events()
        {
            return View();
        }

        /// <summary>
        /// Displays the Favorite view.
        /// </summary>
        /// <returns>The Favorite view.</returns>
        public IActionResult Favorite()
        {
            return View();
        }

        /// <summary>
        /// Displays the Friends view.
        /// </summary>
        /// <returns>The Friends view.</returns>
        public IActionResult Friends()
        {
            return View();
        }

        /// <summary>
        /// Displays the Groups view.
        /// </summary>
        /// <returns>The Groups view.</returns>
        public IActionResult Groups()
        {
            return View();
        }

        /// <summary>
        /// Displays the Help and Support view.
        /// </summary>
        /// <returns>The Help and Support view.</returns>
        [HttpGet($"{UrlConstants.Home}/{UrlConstants.HelpAndSupport}")]
        public IActionResult HelpAndSupport()
        {
            return View(UrlConstants.HelpAndSupport);
        }

        /// <summary>
        /// Displays the Index view.
        /// </summary>
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the Live Chat view.
        /// </summary>
        /// <returns>The Live Chat view.</returns>
        [HttpGet($"{UrlConstants.Home}/{UrlConstants.LiveChat}")]
        public IActionResult LiveChat()
        {
            return View(UrlConstants.LiveChat);
        }

        /// <summary>
        /// Displays the Marketplace view.
        /// </summary>
        /// <returns>The Marketplace view.</returns>
        public IActionResult Marketplace()
        {
            return View();
        }

        /// <summary>
        /// Displays the Messages view.
        /// </summary>
        /// <returns>The Messages view.</returns>
        public IActionResult Messages()
        {
            return View();
        }

        /// <summary>
        /// Displays the MyProfile view.
        /// </summary>
        /// <returns>The MyProfile view.</returns>
        [HttpGet($"{UrlConstants.Home}/{UrlConstants.MyProfile}")]
        public IActionResult MyProfile()
        {
            return View(UrlConstants.MyProfile);
        }

        /// <summary>
        /// Displays the Notifications view.
        /// </summary>
        /// <returns>The Notifications view.</returns>
        public IActionResult Notifications()
        {
            return View();
        }

        /// <summary>
        /// Displays the Privacy view.
        /// </summary>
        /// <returns>The Privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the Video view.
        /// </summary>
        /// <returns>The Video view.</returns>
        public IActionResult Video()
        {
            return View();
        }

        /// <summary>
        /// Displays the Weather view.
        /// </summary>
        /// <returns>The Weather view.</returns>
        public IActionResult Weather()
        {
            return View();
        }

        /// <summary>
        /// Displays the Error view.
        /// </summary>
        /// <returns>The Error view.</returns>
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel vm)
        {
            return View(vm);
        }
    }
}