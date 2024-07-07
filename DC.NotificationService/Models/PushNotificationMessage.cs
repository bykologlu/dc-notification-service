using DC.NotificationService.Enums;
using System.Collections.Generic;

namespace DC.NotificationService.Models
{
    public class PushNotificationMessage
    {
        public PushNotificationMessage()
        {
            Topics = new List<string>();
            DeviceTokens = new List<string>();
        }
        public string Title { get; set; }
        public string Body { get; set; }
        public string? ImageUrl { get; set; }
        public string? ClickAction { get; set; }
        public List<string> Topics { get; set; }
        public List<string> DeviceTokens { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public PushNotificationProvider PushNotificationProvider { get; set; } = PushNotificationProvider.Firebase;

    }
}
