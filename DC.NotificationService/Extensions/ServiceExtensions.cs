using DC.NotificationService.Enums;
using DC.NotificationService.Interfaces;
using DC.NotificationService.Managers;
using DC.NotificationService.Managers.Email;
using DC.NotificationService.Managers.PushNotification;
using DC.NotificationService.Managers.Sms;
using DC.NotificationService.Settings;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DC.NotificationService.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPushNotificationService(this IServiceCollection services)
        {
            services.AddMainService();
            services.AddSingleton<IPushNotificationService, FirebaseManager>();
            return services;
        }

        public static IServiceCollection AddSmsService(this IServiceCollection services)
        {
            services.AddMainService();
            services.AddSingleton<ISmsService, SmsManager>();
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services,IConfiguration configuration = null, EmailSettings settings = null)
        {
            services.AddMainService();

            if (settings == null)
            {
                settings = configuration.GetSection("EmailSettings") as EmailSettings;
            }

            services.AddSingleton<EmailSettings>(settings);

            services.AddTransient<Func<EmailProvider, IEmailService>>(serviceProvider => providerType =>
            {
                switch (providerType)
                {
                    case EmailProvider.Smtp:
                        return serviceProvider.GetRequiredService<SmtpManager>();
                    default:
                        throw new NotImplementedException("The email provider is not registered.");
                }
            });

            services.AddTransient<SmtpManager>();

            return services;
        }

        public static IServiceCollection AddNotificationService(this IServiceCollection services, IConfiguration configuration = null, PushNotificationSettings settings = null)
        {
            services.AddMainService();

            if (settings == null)
            {
                settings = configuration.GetSection("PushNotificationSettings") as PushNotificationSettings;
            }

            services.AddSingleton<PushNotificationSettings>(settings);

            services.AddTransient<Func<PushNotificationProvider, IPushNotificationService>>(serviceProvider => providerType =>
            {
                switch (providerType)
                {
                    case PushNotificationProvider.Firebase:
                    {
                        return serviceProvider.GetRequiredService<FirebaseManager>();
                    }
                    default:
                        throw new NotImplementedException("The push notification provider is not registered.");
                }
            });

            
            return services;
        }
        private static void AddMainService(this IServiceCollection services)
        {
            services.AddSingleton<INotificationServiceFactory, NotificationServiceFactory>();
        }
    }
}
