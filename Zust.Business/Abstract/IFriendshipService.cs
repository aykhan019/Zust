using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    /// <summary>
    /// Interface for managing friendship-related operations, such as adding a new friendship, retrieving followers and followings of a user,
    /// checking if two users are friends, and deleting friendships.
    /// </summary>
    public interface IFriendshipService
    {
        /// <summary>
        /// Adds a new friendship asynchronously.
        /// </summary>
        /// <param name="friendship">The friendship object to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task AddFriendship(Friendship friendship);

        /// <summary>
        /// Retrieves all followers of a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose followers will be retrieved.</param>
        /// <returns>A collection of User objects representing the followers.</returns>
        Task<IEnumerable<User>> GetAllFollowersOfUserAsync(string userId);

        /// <summary>
        /// Retrieves all followings of a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose followings will be retrieved.</param>
        /// <returns>A collection of User objects representing the followings.</returns>
        Task<IEnumerable<User>> GetAllFollowingsOfUserAsync(string userId);

        /// <summary>
        /// Retrieves a friendship asynchronously based on the user IDs.
        /// </summary>
        /// <param name="userId">The ID of one user in the friendship.</param>
        /// <param name="friendId">The ID of the other user in the friendship.</param>
        /// <returns>The Friendship object that matches the user IDs.</returns>
        Task<Friendship> GetFriendshipAsync(string userId, string friendId);

        /// <summary>
        /// Deletes a friendship asynchronously based on the user IDs.
        /// </summary>
        /// <param name="userId">The ID of one user in the friendship.</param>
        /// <param name="friendId">The ID of the other user in the friendship.</param>
        /// <returns>True if the friendship was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteFriendshipAsync(string userId, string friendId);

        /// <summary>
        /// Checks if two users are friends asynchronously.
        /// </summary>
        /// <param name="userId">The ID of one user in the friendship.</param>
        /// <param name="friendId">The ID of the other user in the friendship.</param>
        /// <returns>True if the two users are friends; otherwise, false.</returns>
        Task<bool> IsFriendAsync(string userId, string friendId);

        /// <summary>
        /// Deletes all friendships of a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose friendships will be deleted.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteUserFriendshipsAsync(string userId);
    }
}