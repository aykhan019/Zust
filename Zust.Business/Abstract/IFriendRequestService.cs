using System.Linq.Expressions;
using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    /// <summary>
    /// Interface for managing friend request-related operations, such as adding a new friend request, retrieving friend requests, deleting friend requests, and checking for pending requests.
    /// </summary>
    public interface IFriendRequestService
    {
        /// <summary>
        /// Adds a new friend request asynchronously.
        /// </summary>
        /// <param name="friendRequest">The friend request object to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task AddAsync(FriendRequest friendRequest);

        /// <summary>
        /// Retrieves all friend requests asynchronously.
        /// </summary>
        /// <param name="filter">Optional filter to apply to the friend requests.</param>
        /// <returns>A collection of friend requests.</returns>
        Task<IEnumerable<FriendRequest>> GetAllAsync(Func<FriendRequest, bool>? filter = null);

        /// <summary>
        /// Retrieves a friend request asynchronously based on the specified filter.
        /// </summary>
        /// <param name="filter">The filter expression to select the friend request.</param>
        /// <returns>The friend request object that matches the filter.</returns>
        Task<FriendRequest?> GetAsync(Expression<Func<FriendRequest, bool>> filter);

        /// <summary>
        /// Deletes a friend request asynchronously based on the ID.
        /// </summary>
        /// <param name="id">The ID of the friend request to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// Deletes a friend request asynchronously.
        /// </summary>
        /// <param name="friendRequest">The friend request object to be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteAsync(FriendRequest friendRequest);

        /// <summary>
        /// Updates a friend request asynchronously.
        /// </summary>
        /// <param name="friendRequest">The friend request object to be updated.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task UpdateAsync(FriendRequest friendRequest);

        /// <summary>
        /// Checks if a friend request with the specified sender ID, receiver ID, and status exists asynchronously.
        /// </summary>
        /// <param name="senderId">The ID of the sender.</param>
        /// <param name="receiverId">The ID of the receiver.</param>
        /// <param name="status">The status of the friend request (e.g., "Pending", "Accepted").</param>
        /// <returns>True if a friend request exists with the given parameters; otherwise, false.</returns>
        Task<bool> CheckFriendRequestExistsAsync(string senderId, string receiverId, string status);

        /// <summary>
        /// Checks if a friend request with the specified sender ID, receiver ID, and pending status exists asynchronously.
        /// </summary>
        /// <param name="senderId">The ID of the sender.</param>
        /// <param name="receiverId">The ID of the receiver.</param>
        /// <param name="status">The status of the friend request (e.g., "Pending").</param>
        /// <returns>True if a friend request with the specified parameters and pending status exists; otherwise, false.</returns>
        Task<bool> HasRequestPendingAsync(string senderId, string receiverId, string status);

        /// <summary>
        /// Deletes all friend requests of a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose friend requests will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserFriendRequestsAsync(string userId);
    }
}
