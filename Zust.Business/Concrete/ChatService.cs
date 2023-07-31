using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    /// <summary>
    /// Implementation of the IChatService interface for managing chat-related operations.
    /// </summary>
    public class ChatService : IChatService
    {
        /// <summary>
        /// Private field for accessing the data access layer that handles chat-related operations.
        /// </summary>
        private readonly IChatDal _chatDal;

        /// <summary>
        /// Initializes a new instance of the ChatService class with the specified chat data access layer.
        /// </summary>
        /// <param name="chatDal">The data access layer for chats.</param>
        public ChatService(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

        /// <summary>
        /// Adds a new chat asynchronously.
        /// </summary>
        /// <param name="chat">The Chat object representing the new chat to be added.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task AddChatAsync(Chat chat)
        {
            await _chatDal.AddAsync(chat);
        }

        /// <summary>
        /// Retrieves all chats associated with a user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user whose chats are to be retrieved.</param>
        /// <returns>A collection of Chat objects representing all chats associated with the user.</returns>
        public async Task<IEnumerable<Chat>> GetAllUserChats(string userId)
        {
            var chats = await _chatDal.GetAllAsync(c => c.SenderUserId == userId || c.ReceiverUserId == userId);

            var uniqueChats = chats.GroupBy(c =>
            {
                string id1 = c.SenderUserId;

                string id2 = c.ReceiverUserId;

                return id1.CompareTo(id2) < 0 ? $"{id1}_{id2}" : $"{id2}_{id1}";
            })
            .Select(group => group.First());

            return uniqueChats;
        }

        /// <summary>
        /// Retrieves a chat between two users asynchronously.
        /// </summary>
        /// <param name="senderUserId">The ID of the sender user.</param>
        /// <param name="receiverUserId">The ID of the receiver user.</param>
        /// <returns>The Chat object representing the chat between the specified users. Returns null if not found.</returns>
        public async Task<Chat?> GetChatAsync(string senderUserId, string receiverUserId)
        {
            return await _chatDal.GetAsync(c => c.SenderUserId == senderUserId && c.ReceiverUserId == receiverUserId);
        }

        /// <summary>
        /// Retrieves a chat by its ID asynchronously.
        /// </summary>
        /// <param name="chatId">The ID of the chat to be retrieved.</param>
        /// <returns>The Chat object representing the chat with the given ID. Returns null if not found.</returns>
        public async Task<Chat?> GetChatByIdAsync(string chatId)
        {
            return await _chatDal.GetAsync(c => c.Id == chatId);
        }
    }
}
