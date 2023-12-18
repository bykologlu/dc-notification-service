using DC.NotificationService.Models;

namespace DC.NotificationService.Interfaces
{
    public interface INotificationServiceFactory
    {
        Task SendEmailAsync(EmailMessage emailMessage);
        Task SendSmsAsync();
        Task SendPushNotificationAsync();
    }
}
