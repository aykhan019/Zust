using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetChatMessagesByIdAsync(string chatId);
        Task AddMessageAsync(Message message);
    }
}
