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
        /// <summary>
        /// The service for user-related operations, such as fetching user data and updating user profiles.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// The object mapper used for mapping objects from one type to another.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The ASP.NET Core Identity UserManager for managing user-related operations like user creation and authentication.
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// The ASP.NET Core Identity SignInManager for handling user sign-in and sign-out operations.
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// The service for managing friend requests between users.
        /// </summary>
        private readonly IFriendRequestService _friendRequestService;

        /// <summary>
        /// The service for managing friendships between users.
        /// </summary>
        private readonly IFriendshipService _friendshipService;

        /// <summary>
        /// The service for managing likes on user posts.
        /// </summary>
        private readonly ILikeService _likeService;

        /// <summary>
        /// The service for managing user posts, including creation, retrieval, and deletion.
        /// </summary>
        private readonly IPostService _postService;

        /// <summary>
        /// The service for managing notifications for user activities.
        /// </summary>
        private readonly INotificationService _notificationService;

        /// <summary>
        /// The service for managing comments on user posts.
        /// </summary>
        private readonly ICommentService _commentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="userService">The user service used for user-related operations.</param>
        /// <param name="mapper">The mapper used for object mapping.</param>
        /// <param name="userManager">The UserManager for user-related operations provided by ASP.NET Core Identity.</param>
        /// <param name="signInManager">The SignInManager for user sign-in related operations provided by ASP.NET Core Identity.</param>
        /// <param name="friendRequestService">The service for managing friend requests.</param>
        /// <param name="friendshipService">The service for managing friendships.</param>
        /// <param name="likeService">The service for managing likes.</param>
        /// <param name="postService">The service for managing posts.</param>
        /// <param name="notificationService">The service for managing notifications.</param>
        /// <param name="commentService">The service for managing comments.</param>
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

        /// <summary>
        /// Deletes the user profile and all related data (friend requests, friendships, likes, comments, posts, and notifications).
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
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

        /// <summary>
        /// Validates whether the provided username and password combination is valid for user authentication.
        /// </summary>
        /// <param name="model">The user authentication view model containing the username and password.</param>
        /// <returns>True if the combination is valid, otherwise false.</returns>
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
