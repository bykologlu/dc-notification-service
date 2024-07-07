using DC.NotificationService.Models;
using System.Threading.Tasks;

namespace DC.NotificationService.Interfaces
{
    public interface IPushNotificationService
    {
        Task<PushNotificationRes> SendNotificationAsync(PushNotificationMessage message);

    }
}