using DC.NotificationService.Models;

namespace DC.NotificationService.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}