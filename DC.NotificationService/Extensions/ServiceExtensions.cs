using DC.NotificationService.Enums;
using DC.NotificationService.Interfaces;
using DC.NotificationService.Managers;
using DC.NotificationService.Managers.Email;
using DC.NotificationService.Managers.PushNotification;
using DC.NotificationService.Managers.Sms;
using DC.NotificationService.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace DC.NotificationService.Extensions
{
	public static class ServiceExtensions
    {
        public static IServiceCollection AddPushNotificationService(this IServiceCollection services, IConfiguration configuration = null, PushNotificationSettings settings = null)
        {
            services.AddMainService();

            if (settings == null)
            {
	            settings = configuration.GetSection("PushNotificationSettings") as PushNotificationSettings;
            }

            if (settings != null)
            {
	            services.AddSingleton<PushNotificationSettings>(settings);
            }

            services.AddScoped<Func<PushNotificationProvider, IPushNotificationService>>(serviceProvider => providerType =>
            {
	            switch (providerType)
	            {
		            case PushNotificationProvider.Firebase:
			            return serviceProvider.GetRequiredService<FirebaseManager>();
		            default:
			            throw new NotImplementedException("The push notification provider is not registered.");
	            }
            });

			services.AddScoped<IPushNotificationService, FirebaseManager>();
            return services;
        }

        public static IServiceCollection AddSmsService(this IServiceCollection services)
        {
            services.AddMainService();
            services.AddScoped<ISmsService, SmsManager>();
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services,IConfiguration configuration = null, EmailSettings settings = null)
        {
            services.AddMainService();

            if (settings == null)
            {
				settings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
			}

			if (settings != null)
			{
				services.AddSingleton(settings);
			}
			services.AddScoped<Func<EmailProvider, IEmailService>>(serviceProvider => providerType =>
            {
                switch (providerType)
                {
                    case EmailProvider.Smtp:
                        return serviceProvider.GetRequiredService<SmtpManager>();
                    default:
                        throw new NotImplementedException("The email provider is not registered.");
                }
            });

            services.AddSingleton<SmtpManager>();

            return services;
        }

        private static void AddMainService(this IServiceCollection services)
        {
            services.AddScoped<INotificationServiceFactory, NotificationServiceFactory>();
        }
    }
}
