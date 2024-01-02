using DC.NotificationService.Enums;
using DC.NotificationService.Interfaces;
using DC.NotificationService.Models;

namespace DC.NotificationService.Managers
{
    public class NotificationServiceFactory : INotificationServiceFactory
    {
        private readonly Func<EmailProvider, IEmailService> _emailServiceAccessor;
        private readonly Func<PushNotificationProvider, IPushNotificationService> _pushNotificationAccessor;

        public NotificationServiceFactory(Func<EmailProvider, IEmailService> emailServiceAccessor = null,
											Func<PushNotificationProvider, IPushNotificationService> pushNotificationAccessor = null)
        {
	        _emailServiceAccessor = emailServiceAccessor;
			_pushNotificationAccessor = pushNotificationAccessor;
		}

		public async Task SendEmailAsync(EmailMessage emailMessage)
        {
	        if (_emailServiceAccessor == null) throw new InvalidOperationException("Email service is not configured.");

			await _emailServiceAccessor(emailMessage.EmailProvider).SendEmailAsync(emailMessage);
        }

        public async Task SendSmsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SendPushNotificationAsync(PushNotificationMessage pushNotificationMessage)
        {
	        if (pushNotificationMessage == null) throw new InvalidOperationException("Push Notification service is not configured.");

			await _pushNotificationAccessor(pushNotificationMessage.PushNotificationProvider).SendNotificationAsync(pushNotificationMessage);
        }
    }
}
