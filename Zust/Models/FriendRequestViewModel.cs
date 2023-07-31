namespace Zust.Web.Models
{
    public class FriendRequestViewModel
    {
        public string? Id { get; set; }

        public bool HasFriendRequestPending { get; set; }

        public bool IsFriend { get; set; }
    }
}
