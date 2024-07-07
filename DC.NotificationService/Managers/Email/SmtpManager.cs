using DC.NotificationService.Interfaces;
using DC.NotificationService.Models;
using DC.NotificationService.Settings;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DC.NotificationService.Managers.Email
{
    public class SmtpManager : IEmailService
    {
        private readonly EmailSettings _smtpSettings;

        public SmtpManager(EmailSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            try
            {
				using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
				{
					client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
					client.EnableSsl = _smtpSettings.EnableSsl;

					var mailMessage = new MailMessage
					{
						From = new MailAddress(_smtpSettings.From),
						Subject = emailMessage.Subject,
						Body = emailMessage.Content,
						IsBodyHtml = emailMessage.IsHtml
					};


					foreach (var to in emailMessage.To)
					{
						mailMessage.To.Add(to);
					}

					foreach (var cc in emailMessage.Cc)
					{
						mailMessage.CC.Add(cc);
					}

					foreach (var bcc in emailMessage.Bcc)
					{
						mailMessage.Bcc.Add(bcc);
					}

					await client.SendMailAsync(mailMessage);
				}
			}
            catch(SmtpException ex) 
            {
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

			
        }
    }
}
