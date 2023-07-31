using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task AddAsync(User user)
        {
            await _userDal.AddAsync(user);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userDal.GetAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<bool> UsernameIsTakenAsync(string username)
        {
            var user = await GetUserByUsernameAsync(username);

            return user != null;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var user = await _userDal.GetAsync(u  => u.Id == id);

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userDal.GetAllAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersOtherThanAsync(string userId)
        {
            var users =await GetAllUsersAsync();

            var list = users.ToList();

            list.RemoveAll(u => u.Id == userId);

            return list;
        }

        public async Task DeleteUserByIdAsync(string userId)
        {
            var user = await _userDal?.GetAsync(u => u.Id == userId);

            await _userDal.DeleteAsync(user);
        }
    }
}
