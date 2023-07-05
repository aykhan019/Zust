using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using Zust.Helpers;
using Zust.Models;
using Zust.Web.Helpers;

namespace Zust.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Birthday()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Favorite()
        {
            return View();
        }

        public IActionResult Friends()
        {
            return View();
        }

        public IActionResult Groups()
        {
            return View();
        }

        [HttpGet($"{UrlConstants.Home}/{UrlConstants.HelpAndSupport}")]
        public IActionResult HelpAndSupport()
        {
            return View(UrlConstants.HelpAndSupport);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet($"{UrlConstants.Home}/{UrlConstants.LiveChat}")]
        public IActionResult LiveChat()
        {
            return View(UrlConstants.LiveChat);
        }

        public IActionResult Marketplace()
        {
            return View();
        }

        public IActionResult Messages()
        {
            return View();
        }

        [HttpGet($"{UrlConstants.Home} / {UrlConstants.MyProfile}")]
        public IActionResult MyProfile()
        {
            return View(UrlConstants.MyProfile);
        }

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Video()
        {
            return View();
        }

        public IActionResult Weather()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}