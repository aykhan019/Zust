using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.UserHelpers;

namespace Zust.Web.Controllers.ApiControllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [Route(Routes.UserController)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IFriendshipService _friendshipService;


        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="userService">The user service used for user-related operations.</param>
        public UserController(IUserService userService, IFriendshipService friendshipService)
        {
            _userService = userService;
            _friendshipService = friendshipService;
        }

        /// <summary>
        /// Retrieves the count of all users.
        /// </summary>
        /// <returns>The count of all users.</returns>
        [HttpGet(Routes.GetAllUsersCount)]
        public async Task<ActionResult<int>> GetAllUsersCount()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                return Ok(users.Count());
            }
            catch
            {
                return Ok(0);
            }
        }

        /// <summary>
        /// Retrieves a list of users within the specified range.
        /// </summary>
        /// <param name="startIndex">The start index of the user range.</param>
        /// <param name="userCount">The number of users to retrieve.</param>
        /// <returns>A list of users within the specified range.</returns>
        [HttpGet(Routes.GetUsers)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int startIndex, int userCount)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                var list = users.ToList();

                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                // Excluded the current user to avoid displaying it among Zust Users, as the current user is the one viewing the user list.
                list.RemoveAll(u => u.Id == currentUser.Id);

                var range = new Range(startIndex, startIndex + userCount);

                return Ok(list.Take(range));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet(Routes.GetUser)]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetUsersByText)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByText(string text)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                var list = users.ToList();

                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                // Excluded the current user to avoid displaying it among Zust Users, as the current user is the one viewing the user list.
                list.RemoveAll(u => u.Id == currentUser.Id);

                var filteredUsers = list.Where(u => u.UserName.ToLower().Contains(text.ToLower()));
                return Ok(filteredUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetFollowers)]
        public async Task<ActionResult<IEnumerable<User>>> GetFollowers(string userId)
        {
            try
            {
                var followers = await _friendshipService.GetAllFollowers(userId);

                return Ok(followers.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(Routes.GetFollowings)]
        public async Task<ActionResult<IEnumerable<User>>> GetFollowings(string userId)
        {
            try
            {
                var followings = await _friendshipService.GetAllFollowings(userId);

                return Ok(followings.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
