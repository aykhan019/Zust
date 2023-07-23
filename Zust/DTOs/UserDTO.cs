namespace Zust.Web.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public string? CoverImage { get; set; }
        public bool IsFriend { get; set; }
        public bool HasFriendRequestPending { get; set; }
    }
}
