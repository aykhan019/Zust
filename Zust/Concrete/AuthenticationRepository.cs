using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concrete
{
    /// <summary>
    /// Repository class for authentication-related operations.
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        /// <summary>
        /// The service for user-related operations.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the AuthenticationRepository class.
        /// </summary>
        /// <param name="userService">The user service dependency used for user-related operations.</param>
        public AuthenticationRepository(IUserService userService)
        {
            _userService = userService;
        }

        ///// <summary>
        ///// Retrieves a user from the database if the given username and password are valid user credentials.
        ///// </summary>
        ///// <param name="username">The username of the user.</param>
        ///// <param name="password">The password of the user.</param>
        ///// <returns>The user if the credentials are valid; otherwise, null.</returns>
        //public async Task<User> LoginAsync(string username, string password)
        //{
        //    var user = await _userService.GetUserByUsernameAsync(username);
        //    if (user == null)
        //        return null!;

        //    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //    {
        //        return null!;
        //    }
        //    return user;
        //}

        /// <summary>
        /// Verifies whether a given password matches a stored password hash.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="passwordHash">The stored password hash, represented as a byte array.</param>
        /// <param name="passwordSalt">The stored password salt, represented as a byte array.</param>
        /// <returns>True if the password matches the stored hash; otherwise, false.</returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        ///// <summary>
        ///// Registers a new user in the system with the provided user object and password.
        ///// </summary>
        ///// <param name="user">The user object containing the user's details.</param>
        ///// <param name="password">The password of the user.</param>
        ///// <returns>The registered user object.</returns>
        //public User Register(User user, string password)
        //{
        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    return user;
        //}

        /// <summary>
        /// Creates a password hash and salt for the given password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="passwordHash">The resulting password hash.</param>
        /// <param name="passwordSalt">The resulting password salt.</param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Checks if a user with the given username exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _userService.UserExistsAsync(username);
        }

        public Task<User> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User Register(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
