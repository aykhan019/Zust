using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IFriendshipService
    {
        Task AddFriendship(Friendship friendship);
        Task<IEnumerable<User>> GetAllFollowersOfUserAsync(string userId);
        Task<IEnumerable<User>> GetAllFollowingsOfUserAsync(string userId);
        Task<Friendship> GetFriendshipAsync(string userId, string friendId); 
        Task<bool> DeleteFriendshipAsync(string userId, string friendId);
        Task<bool> IsFriendAsync(string userId, string friendId);
        Task DeleteUserFriendshipsAsync(string userId);
    }
}
