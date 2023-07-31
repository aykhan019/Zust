using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface IMessageService
    {
        /// <summary>
        /// Retrieves all messages of a chat asynchronously based on the chat ID.
        /// </summary>
        /// <param name="chatId">The ID of the chat whose messages will be retrieved.</param>
        /// <returns>A collection of Message objects representing the messages of the chat.</returns>
        Task<IEnumerable<Message>> GetChatMessagesByIdAsync(string chatId);

        /// <summary>
        /// Adds a new message to a chat asynchronously.
        /// </summary>
        /// <param name="message">The message object to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task AddMessageAsync(Message message);
    }
}
