﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}