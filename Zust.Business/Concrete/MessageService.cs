using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    /// <summary>
    /// Represents a service that handles messages.
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// Private field representing the data access layer for managing messages.
        /// </summary>
        private readonly IMessageDal _messageDal;

        /// <summary>
        /// Initializes a new instance of the MessageService class with the specified MessageDal.
        /// </summary>
        /// <param name="messageDal">The data access layer for handling messages.</param>
        public MessageService(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        /// <summary>
        /// Adds a new message asynchronously.
        /// </summary>
        /// <param name="message">The Message object representing the message to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task AddMessageAsync(Message message)
        {
            await _messageDal.AddAsync(message);
        }

        /// <summary>
        /// Retrieves all messages of a chat by its ID asynchronously.
        /// </summary>
        /// <param name="chatId">The ID of the chat.</param>
        /// <returns>A collection of Message objects representing the messages in the chat.</returns>
        public async Task<IEnumerable<Message>> GetChatMessagesByIdAsync(string chatId)
        {
            return await _messageDal.GetAllAsync(m => m.ChatId == chatId);
        }

        /// <summary>
        /// Retrieves the last message from the specified chat asynchronously.
        /// </summary>
        /// <param name="chat">The chat from which to retrieve the last message.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the last message in the chat.</returns>
        public async Task<Message> GetLastMessageAsync(Chat chat)
        {
            var messages = await GetChatMessagesByIdAsync(chat.Id);

            Message latestMessage = messages.OrderByDescending(obj => obj.DateSent).FirstOrDefault();

            return latestMessage;
        }
    }
}