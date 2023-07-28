using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entities.Models;

namespace Zust.Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationService(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task AddAsync(Notification notification)
        {
            await _notificationDal.AddAsync(notification);
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsOfUserAsync(string userId)
        {
            return await _notificationDal.GetAllAsync(n => n.ToUserId == userId);
        }

        public async Task<Notification?> GetNotificationByIdAsync(string notificationId)
        {
            return await _notificationDal.GetAsync(n => n.Id == notificationId);
        }

        public async Task<int> GetUnreadNotificationCountAsync(string userId)
        {
            var notifications = await GetAllNotificationsOfUserAsync(userId);

            return notifications.Where(n => n.IsRead == false).Count();
        }

        public async Task UpdateNotificationIsRead(string notificationid)
        {
            var post = await GetNotificationByIdAsync(notificationid);

            post.IsRead = true;

            await _notificationDal.UpdateAsync(post);
        }
    }
}
