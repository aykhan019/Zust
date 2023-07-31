using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    /// <summary>
    /// Interface for managing chat-related operations, such as retrieving chat details, adding a new chat, and retrieving user chats.
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// Retrieves a chat asynchronously based on the sender and receiver user IDs.
        /// </summary>
        /// <param name="senderUserId">The ID of the sender user.</param>
        /// <param name="receiverUserId">The ID of the receiver user.</param>
        /// <returns>The chat object if found, otherwise null.</returns>
        Task<Chat> GetChatAsync(string senderUserId, string receiverUserId);

        /// <summary>
        /// Adds a new chat asynchronously.
        /// </summary>
        /// <param name="chat">The chat object to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task AddChatAsync(Chat chat);

        /// <summary>
        /// Retrieves a chat asynchronously based on the chat ID.
        /// </summary>
        /// <param name="chatId">The ID of the chat to retrieve.</param>
        /// <returns>The chat object if found, otherwise null.</returns>
        Task<Chat> GetChatByIdAsync(string chatId);

        /// <summary>
        /// Retrieves all user chats asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of user chats.</returns>
        Task<IEnumerable<Chat>> GetAllUserChats(string userId);
    }
}
