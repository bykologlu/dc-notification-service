
# DC Notification Service

## About The Project

The DC Notification Service is a .NET 7 based library designed to simplify the process of sending various types of notifications such as emails, SMS, and push notifications. It abstracts the complexity of dealing with different notification providers and offers a unified interface to send notifications with just a few lines of code.

### Features

- **Extensible Notification Providers:** Easily integrate with any email, SMS, or push notification service provider.
- **Runtime Provider Selection:** Choose which notification service to use at runtime, offering flexibility for different environments and scenarios.
- **Support for Multiple Notification Types:** Emails with support for HTML content, multiple recipients, CC, and BCC; SMS notifications; Push notifications.

## Getting Started

To get started with the DC Notification Service, install the package via NuGet, configure your service provider, and you're ready to send notifications.

### Installation

Install the package with NuGet:

```bash
dotnet add package DC.NotificationService
```

### Configuration

Configure the services in your `Startup.cs`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Configure your notification services here
    services.AddEmailService(settings:new EmailSettings()
    {
        EnableSsl = true,
        Host = "smtp-host",
        Port = 2525,
        Password = "mail-password",
        Username = "mail-username",
        From = "test@mail.com"
    });
}
```

### Usage

Inject the `INotificationServiceFactory` to create and send notifications:

```csharp
public class NotificationController : ControllerBase
{
    private readonly INotificationServiceFactory _notificationFactory;

    public NotificationController(INotificationServiceFactory notificationFactory)
    {
        _notificationFactory = notificationFactory;
    }

    public async Task<IActionResult> SendNotification()
    {
        EmailMessage emailMessage = new EmailMessage
        {
            To = new List<string> { "test@mail.com" },
            Subject = "Welcome to DC Notification Service",
            Content = "<p>This is a test email.</p>",
            IsHtml = true
        };

        await _notificationFactory.SendEmail(emailMessage);

        return Ok("Notification sent successfully.");
    }
}
```

## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

Don't forget to give the project a star :)  Thanks again!

## To-Do

- [ ] Integrate Twilio for SMS notifications.
- [ ] Integrate MailChimp for email services.
- [ ] Integrate Firebase for push notifications.
