using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using Zust.Helpers;
using Zust.Models;

namespace Zust.Controllers
{
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

        [HttpGet($"{Constants.Home}/{Constants.HelpAndSupport}")]
        public IActionResult HelpAndSupport()
        {
            return View(Constants.HelpAndSupport);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet($"{Constants.Home}/{Constants.LiveChat}")]
        public IActionResult LiveChat()
        {
            return View(Constants.LiveChat);
        }

        public IActionResult Marketplace()
        {
            return View();
        }

        public IActionResult Messages()
        {
            return View();
        }

        [HttpGet($"{Constants.Home}/{Constants.MyProfile}")]
        public IActionResult MyProfile()
        {
            return View(Constants.MyProfile);
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