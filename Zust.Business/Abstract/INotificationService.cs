using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface INotificationService
    {
        Task AddAsync(Notification notification);
        Task<IEnumerable<Notification>> GetAllNotificationsOfUserAsync(string userId);
        Task<Notification> GetNotificationByIdAsync(string notificationId);
        Task<int> GetUnreadNotificationCountAsync(string userId);
        Task UpdateNotificationIsReadAsync(string notificationid);
        Task DeleteUserNotificationsAsync(string userId);
    }
}
