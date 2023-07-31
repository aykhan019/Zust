using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IUserService
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> UsernameIsTakenAsync(string username);
        Task AddAsync(User user);
        Task<User?> GetUserByIdAsync(string id);
        Task UpdateAsync(User user); 
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllUsersOtherThanAsync(string userId);
        Task DeleteUserByIdAsync(string userId);
    }
}   
