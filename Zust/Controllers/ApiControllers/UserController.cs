using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Entities.Models;
using Zust.Web.Abstract;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;
using Zust.Web.Migrations;
using Zust.Web.Models;

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

        private readonly IFriendRequestService _friendRequestService;

        private readonly IMediaService _mediaService;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="userService">The user service used for user-related operations.</param>
        public UserController(IUserService userService, IFriendshipService friendshipService, IFriendRequestService friendRequestService, IMediaService mediaService)
        {
            _userService = userService;
            _friendshipService = friendshipService;
            _friendRequestService = friendRequestService;
            _mediaService = mediaService;
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

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

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

                if (user == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

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

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

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
                var followers = await _friendshipService.GetAllFollowersOfUserAsync(userId);

                return Ok(followers.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetFollowersCount)]
        public async Task<ActionResult<int>> GetFollowersCount(string userId)
        {
            try
            {
                var followers = await _friendshipService.GetAllFollowersOfUserAsync(userId);

                var count = followers.Count();

                return Ok(count);
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
                var followings = await _friendshipService.GetAllFollowingsOfUserAsync(userId);

                return Ok(followings.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetFollowingsCount)]
        public async Task<ActionResult<int>> GetFollowingsCount(string userId)
        {
            try
            {
                var followings = await _friendshipService.GetAllFollowingsOfUserAsync(userId);

                var count = followings.Count();

                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.RemoveFriend)]
        public async Task<IActionResult> RemoveFriend(string friendId)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var currentUserId = currentUser.Id;

                var friendRequest = await _friendRequestService.GetAsync(fr => fr.SenderId == currentUserId && fr.ReceiverId == friendId);

                if (friendRequest == null)
                {
                    return NotFound(Errors.FriendRequestNotFound);
                }

                await _friendRequestService.DeleteAsync(friendRequest);

                var deleted = await _friendshipService.DeleteFriendshipAsync(currentUserId, friendId);

                if (!deleted)
                {
                    return BadRequest(Errors.FriendRequestNotFound);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetCurrentUser)]
        public async Task<ActionResult<User?>> GetCurrentUser()
        {
            try
            {
                var user = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (user == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.UpdateProfileImage)]
        public async Task<IActionResult> UpdateProfileImage([FromForm] UpdateProfileViewModel model)
        {
            try
            {
                var imageFile = model.MediaFile;

                var userId = model.UserId;

                // Check if the file and userId exist and are valid
                if (imageFile != null && imageFile.Length > 0 && !string.IsNullOrEmpty(userId))
                {
                    var mediaUrl = await _mediaService.UploadMediaAsync(imageFile);

                    if (mediaUrl != string.Empty)
                    {
                        var user = await _userService.GetUserByIdAsync(userId);

                        if (user != null)
                        {
                            user.ImageUrl = mediaUrl;

                            await _userService.UpdateAsync(user);

                            return Ok();
                        }

                        return NotFound(Errors.UserNotFound);
                    }

                    return BadRequest(Errors.ImageUploadError);
                }

                return BadRequest(Errors.InvalidRequestData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
