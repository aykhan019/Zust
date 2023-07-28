using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstract
{
    public interface INotificationService
    {
        Task AddAsync(Notification notification);
        Task<IEnumerable<Notification>> GetAllNotificationsOfUserAsync(string userId);
        Task<Notification> GetNotificationByIdAsync(string notificationId);
        Task<int> GetUnseenNotificationCountAsync(string userId);
    }
}
