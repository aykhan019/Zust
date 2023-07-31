using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entities.Models;
using Zust.Web.Abstract;
using Zust.Web.DTOs;
using Zust.Web.Extensions;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Helpers.Utilities;
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

        private readonly INotificationService _notificationService;

        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="userService">The user service used for user-related operations.</param>
        public UserController(IUserService userService, IFriendshipService friendshipService, IFriendRequestService friendRequestService, IMediaService mediaService, INotificationService notificationService, IMapper mapper)
        {
            _userService = userService;

            _friendshipService = friendshipService;

            _friendRequestService = friendRequestService;

            _mediaService = mediaService;

            _notificationService = notificationService;

            _mapper = mapper;
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
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var currentUserId = currentUser.Id;

                var users = await _userService.GetAllUsersOtherThanAsync(currentUserId);

                var userDTOs = _mapper.Map<List<UserDTO>>(users);

                userDTOs.ForEach(async user =>
                {
                    user.IsFriend = await _friendshipService.IsFriendAsync(currentUserId, user.Id);
                    if (!user.IsFriend)
                    {
                        user.HasFriendRequestPending = await _friendRequestService.HasRequestPendingAsync(currentUserId, user.Id, Status.Pending);
                    }
                });

                var range = new Range(startIndex, startIndex + userCount);

                return Ok(userDTOs.Take(range));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //private async Task addRandom()
        //{
        //    var random = new Random();
        //    var list = await _userService.GetAllUsersAsync();

        //    foreach (var user in list)
        //    {
        //        var allUsers = list.Where(u => u.Id != user.Id).ToList();
        //        var userCount = random.Next(0, allUsers.Count());
        //        var pendingRequestCount = (int)(userCount * 0.07);
        //        var friendCount = userCount - pendingRequestCount;

        //        var users = allUsers.ToList().GetRandomElements(userCount);

        //        for (int i = 0; i < friendCount; i++)
        //        {
        //            var friendToSendRequest = users[i];

        //            var requestDate = GenerateRandomDate(new DateTime(2023, 1, 1));

        //            var fr = new FriendRequest()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                SenderId = user.Id,
        //                ReceiverId = friendToSendRequest.Id,
        //                RequestDate = GenerateRandomDate(new DateTime(2023, 1, 1)),
        //                Status = Status.Accepted,
        //            };

        //            await _friendRequestService.AddAsync(fr);

        //            var ntfc = new Notification()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                Date = requestDate,
        //                IsRead = true,
        //                FromUser = user,
        //                FromUserId = user.Id,
        //                ToUser = friendToSendRequest,
        //                ToUserId = friendToSendRequest.Id,
        //                Message = NotificationType.GetNewFriendRequestMessage(user.UserName),
        //            };

        //            await _notificationService.AddAsync(ntfc);

        //            var friendShip = new Friendship()
        //            {
        //                FriendshipId = Guid.NewGuid().ToString(),

        //                FriendId = fr.ReceiverId,

        //                UserId = fr.SenderId
        //            };

        //            await _friendshipService.AddFriendship(friendShip);

        //            var ntfc2 = new Notification()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                Date = requestDate.AddDays(random.Next(35)),
        //                IsRead = true,
        //                Message = NotificationType.GetFriendRequestAcceptedMessage(friendToSendRequest.UserName),
        //                FromUser = friendToSendRequest,
        //                FromUserId = friendToSendRequest.Id,
        //                ToUser = user,
        //                ToUserId = user.Id
        //            };

        //            await _notificationService.AddAsync(ntfc2);
        //        }

        //        for (int i = 0; i < pendingRequestCount; i++)
        //        {
        //            var friendToSendRequest = users[i + friendCount];

        //            var requestDate = GenerateRandomDate(new DateTime(2023, 1, 1));

        //            var fr = new FriendRequest()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                SenderId = user.Id,
        //                ReceiverId = friendToSendRequest.Id,
        //                RequestDate = requestDate,
        //                Status = Status.Pending,
        //            };

        //            await _friendRequestService.AddAsync(fr);

        //            var ntfc = new Notification()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                Date = requestDate,
        //                IsRead = true,
        //                Message = NotificationType.GetNewFriendRequestMessage(user.UserName),
        //                FromUser = user,
        //                FromUserId = user.Id,
        //                ToUser = friendToSendRequest,
        //                ToUserId = friendToSendRequest.Id
        //            };

        //            await _notificationService.AddAsync(ntfc);
        //        }
        //    }
        //}

        //private static DateTime GenerateRandomDate(DateTime startDate)
        //{
        //    DateTime endDate = DateTime.Today;
        //    Random random = new Random();
        //    int range = (endDate - startDate).Days;

        //    return startDate.AddDays(random.Next(range));
        //}

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet(Routes.GetUser)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser(string id)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var userDTO = _mapper.Map<UserDTO>(user);

                userDTO.IsFriend = await _friendshipService.IsFriendAsync(currentUser.Id, user.Id);

                if (!userDTO.IsFriend)
                {
                    userDTO.HasFriendRequestPending = await _friendRequestService.HasRequestPendingAsync(currentUser.Id, user.Id, Status.Pending);
                }

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetUsersByText)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByText(string text)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var users = await _userService.GetAllUsersOtherThanAsync(currentUser.Id);

                var filteredUsers = users.Where(u => u.UserName.ToLower().Contains(text.ToLower()));

                var userDTOs = _mapper.Map<List<UserDTO>>(filteredUsers);

                userDTOs.ForEach(async user =>
                {
                    user.IsFriend = await _friendshipService.IsFriendAsync(currentUser.Id, user.Id);
                    if (!user.IsFriend)
                    {
                        user.HasFriendRequestPending = await _friendRequestService.HasRequestPendingAsync(currentUser.Id, user.Id, Status.Pending);
                    }
                });

                return Ok(userDTOs);
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

        [HttpGet(Routes.GetFollowersInRange)]
        public async Task<ActionResult<IEnumerable<User>>> GetFollowersInRange(int startIndex, int takeCount)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var currentUserId = currentUser.Id;

                var followers = await _friendshipService.GetAllFollowersOfUserAsync(currentUserId);

                var userDTOs = _mapper.Map<List<UserDTO>>(followers);

                var range = new Range(startIndex, startIndex + takeCount);

                return Ok(followers.Take(range));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetRandomFollowers)]
        public async Task<ActionResult<IEnumerable<User>>> GetRandomFollowers(string userId)
        {
            try
            {
                var followers = await _friendshipService.GetAllFollowersOfUserAsync(userId);

                return Ok(followers.ToList().GetRandomElements(Constants.RandomFollowerCount));
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

        [HttpGet(Routes.GetFollowingsInRange)]
        public async Task<ActionResult<IEnumerable<User>>> GetFollowingsInRange(int startIndex, int takeCount)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                if (currentUser == null)
                {
                    return NotFound(Errors.UserNotFound);
                }

                var currentUserId = currentUser.Id;

                var followings = await _friendshipService.GetAllFollowingsOfUserAsync(currentUserId);

                var userDTOs = _mapper.Map<List<UserDTO>>(followings);

                var range = new Range(startIndex, startIndex + takeCount);

                return Ok(followings.Take(range));
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

                if (friendRequest != null)
                {
                    await _friendRequestService.DeleteAsync(friendRequest);
                }

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

        [HttpPost(Routes.RemoveFollower)]
        public async Task<IActionResult> RemoveFollower(string friendId)
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

                if (friendRequest != null)
                {
                    await _friendRequestService.DeleteAsync(friendRequest);
                }

                var deleted = await _friendshipService.DeleteFriendshipAsync(friendId, currentUserId);

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

        [HttpGet(Routes.GetUsersWithTodayBirthday)]
        public async Task<IActionResult> GetUsersWithTodayBirthday(string userId)
        {
            try
            {
                var users = await _userService.GetAllUsersOtherThanAsync(userId);

                var today = DateTime.Today;

                var todayBirthdayUsers = users.Where(user => user.Birthday.Day == today.Day && user.Birthday.Month == today.Month);

                return Ok(todayBirthdayUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetUsersWithRecentBirthday)]
        public async Task<IActionResult> GetUsersWithRecentBirthday(string userId)
        {
            try
            {
                var users = await _userService.GetAllUsersOtherThanAsync(userId);

                var today = DateTime.Today;

                DateTime startDate = today.AddDays(-Constants.BirthdayRange); // 7 days ago (excluding today)

                DateTime endDate = today.AddDays(-1);   // Yesterday (excluding today)

                var birthdayUsersInRange = users.Where(user => user.Birthday >= startDate && user.Birthday <= endDate);

                return Ok(birthdayUsersInRange);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Routes.GetUsersWithComingBirthday)]
        public async Task<IActionResult> GetUsersWithComingBirthday(string userId)
        {
            try
            {
                var users = await _userService.GetAllUsersOtherThanAsync(userId);

                var today = DateTime.Today;

                DateTime startDate = today.AddDays(1);   // Tomorrow (excluding today)

                DateTime endDate = today.AddDays(Constants.BirthdayRange);     // 7 days forward

                var birthdayUsersInRange = users.Where(user => user.Birthday >= startDate && user.Birthday <= endDate);

                return Ok(birthdayUsersInRange);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
