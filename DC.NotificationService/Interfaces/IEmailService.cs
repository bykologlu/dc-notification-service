using DC.NotificationService.Models;
using System.Threading.Tasks;

namespace DC.NotificationService.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}