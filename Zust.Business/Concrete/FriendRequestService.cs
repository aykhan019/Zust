using System.Linq.Expressions;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    /// <summary>
    /// Represents a service to manage friend requests.
    /// </summary>
    public class FriendRequestService : IFriendRequestService
    {
        /// <summary>
        /// Private field representing the data access layer for friend requests.
        /// </summary>
        private readonly IFriendRequestDal _friendRequestDal;

        /// <summary>
        /// Initializes a new instance of the FriendRequestService class with the specified dependency.
        /// </summary>
        /// <param name="friendRequestDal">The data access layer for FriendRequest entities.</param>
        public FriendRequestService(IFriendRequestDal friendRequestDal)
        {
            _friendRequestDal = friendRequestDal;
        }

        /// <summary>
        /// Adds a new friend request asynchronously.
        /// </summary>
        /// <param name="friendRequest">The FriendRequest object representing the new friend request to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task AddAsync(FriendRequest friendRequest)
        {
            await _friendRequestDal.AddAsync(friendRequest);
        }

        /// <summary>
        /// Deletes a friend request by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the friend request to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(string id)
        {
            await _friendRequestDal.DeleteAsync(await GetAsync(f => f.Id == id));
        }

        /// <summary>
        /// Deletes a friend request asynchronously.
        /// </summary>
        /// <param name="friendRequest">The FriendRequest object representing the friend request to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(FriendRequest friendRequest)
        {
            await _friendRequestDal.DeleteAsync(friendRequest);
        }

        /// <summary>
        /// Updates an existing friend request asynchronously.
        /// </summary>
        /// <param name="friendRequest">The FriendRequest object representing the friend request to be updated.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(FriendRequest friendRequest)
        {
            await _friendRequestDal.UpdateAsync(friendRequest);
        }

        /// <summary>
        /// Retrieves all friend requests asynchronously, optionally filtered by a predicate.
        /// </summary>
        /// <param name="filter">The predicate to filter the friend requests (optional).</param>
        /// <returns>A collection of FriendRequest objects representing all friend requests.</returns>
        public async Task<IEnumerable<FriendRequest>> GetAllAsync(Func<FriendRequest, bool>? filter = null)
        {
            var items = await _friendRequestDal.GetAllAsync();

            return filter == null ? items : items.Where(filter);
        }

        /// <summary>
        /// Checks if a friend request with the given parameters exists asynchronously.
        /// </summary>
        /// <param name="senderId">The ID of the sender of the friend request.</param>
        /// <param name="receiverId">The ID of the receiver of the friend request.</param>
        /// <param name="status">The status of the friend request.</param>
        /// <returns>True if the friend request exists; otherwise, false.</returns>
        public async Task<bool> HasRequestPendingAsync(string senderId, string receiverId, string status)
        {
            var friendRequest = await _friendRequestDal.GetAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId && fr.Status == status);

            return friendRequest != null;
        }

        /// <summary>
        /// Retrieves a friend request asynchronously based on the given filter.
        /// </summary>
        /// <param name="filter">The filter expression to select the friend request.</param>
        /// <returns>The FriendRequest object that matches the given filter, or null if not found.</returns>
        public Task<FriendRequest?> GetAsync(Expression<Func<FriendRequest, bool>> filter)
        {
            return _friendRequestDal.GetAsync(filter);
        }

        /// <summary>
        /// Checks if a friend request with the given parameters exists asynchronously.
        /// </summary>
        /// <param name="senderId">The ID of the sender of the friend request.</param>
        /// <param name="receiverId">The ID of the receiver of the friend request.</param>
        /// <param name="status">The status of the friend request.</param>
        /// <returns>True if the friend request exists; otherwise, false.</returns>
        public async Task<bool> CheckFriendRequestExistsAsync(string senderId, string receiverId, string status)
        {
            var friendRequest = await GetAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId && fr.Status == status);

            return friendRequest != null;
        }

        /// <summary>
        /// Deletes all friend requests associated with a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose friend requests are to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteUserFriendRequestsAsync(string userId)
        {
            var friendRequests = await _friendRequestDal.GetAllAsync(fr => fr.SenderId == userId || fr.ReceiverId == userId);

            if (friendRequests != null)
            {
                foreach (var friendRequest in friendRequests)
                {
                    await DeleteAsync(friendRequest);
                }
            }
        }
    }
}
