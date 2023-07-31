using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetChatMessagesByIdAsync(string chatId);
        Task AddMessageAsync(Message message);
    }
}
