using DC.NotificationService.Models;
using System.Threading.Tasks;

namespace DC.NotificationService.Interfaces
{
    public interface INotificationServiceFactory
    {
        Task SendEmailAsync(EmailMessage emailMessage);
        Task SendSmsAsync();
        Task SendPushNotificationAsync(PushNotificationMessage pushNotificationMessage);
    }
}
