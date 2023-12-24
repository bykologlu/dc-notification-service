using DC.NotificationService.Enums;
using DC.NotificationService.Interfaces;
using DC.NotificationService.Models;

namespace DC.NotificationService.Managers
{
    public class NotificationServiceFactory : INotificationServiceFactory
    {
        private readonly Func<EmailProvider, IEmailService> _emailServiceAccessor;
        private readonly Func<PushNotificationProvider, IPushNotificationService> _pushNotificationAccessor;

        public NotificationServiceFactory(Func<EmailProvider, IEmailService> emailServiceAccessor, 
                                          Func<PushNotificationProvider, IPushNotificationService> pushNotificationAccessor)
        {
            _emailServiceAccessor = emailServiceAccessor;
            _pushNotificationAccessor = pushNotificationAccessor;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            await _emailServiceAccessor(emailMessage.EmailProvider).SendEmailAsync(emailMessage);
        }

        public async Task SendSmsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SendPushNotificationAsync(PushNotificationMessage pushNotificationMessage)
        {
            await _pushNotificationAccessor(pushNotificationMessage.PushNotificationProvider).SendNotificationAsync(pushNotificationMessage);
        }
    }
}
