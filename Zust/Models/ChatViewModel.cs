using Zust.Entities.Models;

namespace Zust.Web.Models
{
    /// <summary>
    /// Represents a view model for the chat functionality, containing information about the users involved in the chat and the chat itself.
    /// </summary>
    public class ChatViewModel
    {
        /// <summary>
        /// Gets or sets the user to chat with.
        /// </summary>
        public User? UserToChat { get; set; }

        /// <summary>
        /// Gets or sets the current authenticated user.
        /// </summary>
        public User? CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the chat instance for the current authenticated user.
        /// </summary>
        public Chat? Chat { get; set; }

        /// <summary>
        /// Gets or sets the chat instance for the other user involved in the chat.
        /// </summary>
        public Chat? ChatForOtherUser { get; set; }
    }
}
