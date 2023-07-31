using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageService(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messageDal.AddAsync(message);
        }

        public async Task<IEnumerable<Message>> GetChatMessagesByIdAsync(string chatId)
        {
            return await _messageDal.GetAllAsync(m => m.ChatId == chatId);
        }
    }
}
