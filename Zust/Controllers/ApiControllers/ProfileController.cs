using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Entities;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Models;

namespace Zust.Web.Controllers.ApiControllers
{
    /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    [ApiController]
    [Route(Routes.ProfileController)]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IFriendRequestService _friendRequestService;
        private readonly IFriendshipService _friendshipService;
        private readonly ILikeService _likeService;
        private readonly IPostService _postService;
        private readonly INotificationService _notificationService;
        private readonly ICommentService _commentService;

        /// <summary>
        /// Initializes a new instance of the ProfileController class.
        /// </summary>
        /// <param name="userService">The user service used for user-related operations.</param>
        /// <param name="mapper">The mapper used for object mapping.</param>
        public ProfileController(IUserService userService,
                                 IMapper mapper,
                                 UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 IFriendRequestService friendRequestService,
                                 IFriendshipService friendshipService,
                                 ILikeService likeService,
                                 IPostService postService,
                                 INotificationService notificationService,
                                 ICommentService commentService)
        {
            _userService = userService;

            _mapper = mapper;

            _userManager = userManager;

            _signInManager = signInManager;

            _friendRequestService = friendRequestService;

            _friendshipService = friendshipService;

            _likeService = likeService;

            _postService = postService;

            _notificationService = notificationService;

            _commentService = commentService;
        }

        /// <summary>
        /// Updates the user profile with the provided data.
        /// </summary>
        /// <param name="updatedProfile">The updated user profile data.</param>
        /// <returns>The appropriate ActionResult based on the update result.</returns>
        [HttpPut(Routes.UpdateProfile)]
        public async Task<IActionResult> UpdateProfile(UserProfile updatedProfile)
        {
            // Retrieve the existing user profile from the database based on the ID
            var user = await _userService.GetUserByUsernameAsync(updatedProfile.OldUserName);

            if (user == null)
            {
                return NotFound();
            }

            // Update the user profile with the new values
            _mapper.Map(updatedProfile, user);

            try
            {
                await _userService.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Return a 200 OK response indicating success
            return Ok();
        }

        [HttpDelete(Routes.DeleteProfile)]
        public async Task<IActionResult> DeleteProfile(string userId)
        {
            try
            {
                // Delete Friend Requests
                await _friendRequestService.DeleteUserFriendRequestsAsync(userId);

                // Delete Friendships
                await _friendshipService.DeleteUserFriendshipsAsync(userId);

                // Delete Likes
                await _likeService.DeleteUserLikesAsync(userId);

                // Delete Post Comments
                await _commentService.DeleteUserCommentsAsync(userId);

                // Delete Comments of All User Posts
                await _postService.DeleteUserPostCommentsAsync(userId);

                // Delete Likes of All User Posts
                await _postService.DeleteUserPostLikesAsync(userId);

                // Delete Posts
                await _postService.DeleteUserPostsAsync(userId);

                // Delete Notifications
                await _notificationService.DeleteUserNotificationsAsync(userId);

                // Delete User
                await _userService.DeleteUserByIdAsync(userId);

                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Routes.IsUsernameAndPasswordValid)]
        public async Task<bool> IsUsernameAndPasswordValid([FromBody] UserAuthenticationViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

                if (signInResult.Succeeded)
                {
                    // Username and password are valid
                    return true;
                }
            }

            // Username or password is invalid
            return false;
        }
    }
}
