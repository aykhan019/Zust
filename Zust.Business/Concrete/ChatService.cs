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
    public class ChatService : IChatService
    {
        private readonly IChatDal _chatDal;

        public ChatService(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

        public async Task AddChatAsync(Chat chat)
        {
            await _chatDal.AddAsync(chat);
        }

        public async Task<Chat?> GetChatAsync(string senderUserId, string receiverUserId)
        {
            return await _chatDal.GetAsync(c => c.SenderUserId == senderUserId && c.ReceiverUserId == receiverUserId);
        }

        public async Task<Chat?> GetChatByIdAsync(string chatId)
        {
            return await _chatDal.GetAsync(c => c.Id == chatId);
        }
    }
}
