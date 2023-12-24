using DC.NotificationService.Models;

namespace DC.NotificationService.Interfaces
{
    public interface IPushNotificationService
    {
        Task<PushNotificationRes> SendNotificationAsync(PushNotificationMessage message);

    }
}