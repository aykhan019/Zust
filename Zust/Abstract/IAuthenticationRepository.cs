using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstract
{
    public interface IAuthenticationRepository
    {
        Task<User> LoginAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
        User Register(User user, string password);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
