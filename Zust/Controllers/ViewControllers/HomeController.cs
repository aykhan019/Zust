using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Web.Helpers.ConstantHelpers;

namespace Zust.Web.Controllers.ViewControllers
{
    /// <summary>
    /// Controller for managing home-related views and actions.
    /// </summary>
    [Authorize]
    [Controller]
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets the user service used by the controller.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the HomeController class with the specified user service.
        /// </summary>
        /// <param name="userService">The user service to be used by the controller.</param>
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Displays the Birthday view.
        /// </summary>
        /// <returns>The Birthday view.</returns>
        public IActionResult Birthday()
        {
            return View();
        }

        /// <summary>
        /// Renders the user profile view.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user profile view.</returns>
        public async Task<IActionResult> Users(string id = Constants.StringEmpty)
        {
            if (id == Constants.StringEmpty)
            {
                return View();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(Routes.UserProfile, user);
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
        [HttpGet($"{Routes.Home}/{Routes.HelpAndSupport}")]
        public IActionResult HelpAndSupport()
        {
            return View(Routes.HelpAndSupport);
        }

        /// <summary>
        /// Displays the Help and Support view.
        /// </summary>
        /// <returns>The Help and Support view.</returns>
        [HttpGet($"{Routes.Home}/{Routes.FriendRequests}")]
        public IActionResult FriendRequests()
        {
            return View(Routes.FriendRequests);
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
        [HttpGet($"{Routes.Home}/{Routes.LiveChat}")]
        public IActionResult LiveChat()
        {
            return View(Routes.LiveChat);
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
        [HttpGet($"{Routes.Home}/{Routes.MyProfile}")]
        public IActionResult MyProfile()
        {
            return View(Routes.MyProfile);
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
        public IActionResult Error()
        {
            return View();
        }
    }
}