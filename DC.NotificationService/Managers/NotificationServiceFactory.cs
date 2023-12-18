using DC.NotificationService.Enums;
using DC.NotificationService.Interfaces;
using DC.NotificationService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DC.NotificationService.Managers
{
    public class NotificationServiceFactory : INotificationServiceFactory
    {
        private readonly Func<EmailProvider, IEmailService> _emailServiceAccessor;

        public NotificationServiceFactory(Func<EmailProvider, IEmailService> emailServiceAccessor)
        {
            _emailServiceAccessor = emailServiceAccessor;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            await _emailServiceAccessor(emailMessage.EmailProvider).SendEmailAsync(emailMessage);
        }

        public async Task SendSmsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SendPushNotificationAsync()
        {
            throw new NotImplementedException();
        }
    }
}
