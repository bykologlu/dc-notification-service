using DC.NotificationService.Enums;
using System.Collections.Generic;

namespace DC.NotificationService.Models
{
    public class EmailMessage
    {
        public List<string> To { get; set; } = new List<string>();
        public List<string> Cc { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHtml { get; set; }
        public EmailProvider EmailProvider { get; set; } = EmailProvider.Smtp;
    }
}
