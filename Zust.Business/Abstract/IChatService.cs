using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IChatService
    {
        Task<Chat> GetChatAsync(string senderUserId, string receiverUserId);
        Task AddChatAsync(Chat chat);
        Task<Chat> GetChatByIdAsync(string chatId);
        Task<IEnumerable<Chat>> GetAllUserChats(string userId);
    }
}
