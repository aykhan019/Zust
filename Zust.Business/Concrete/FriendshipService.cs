using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipDal _friendshipDal;

        public FriendshipService(IFriendshipDal friendshipDal)
        {
            _friendshipDal = friendshipDal;
        }

        public async Task AddFriendToUser(Friendship friendship)
        {
            await _friendshipDal.AddAsync(friendship);
        }
    }
}
