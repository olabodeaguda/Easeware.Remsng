using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Easeware.Remsng.Common.Utilities;

namespace Easeware.Remsng.Services.Implementations
{
    public class EmailService : IEmailService
    {
        EmailConfiguration emailConfiguration;
        public EmailService(EmailConfiguration _emailConfiguration)
        {
            emailConfiguration = _emailConfiguration;
        }

        public async Task<bool> Send(NotificationModel notificationModel)
        {
            try
            {
                SmtpClient client = new SmtpClient(emailConfiguration.SmtpServer);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailConfiguration.SmtpUsername,
                    emailConfiguration.SmtpPassword);
                client.Port = emailConfiguration.SmtpPort;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(notificationModel.FromEmail);

                foreach (var tm in notificationModel.ToEmails)
                {
                    mailMessage.To.Add(tm);
                }

                if (notificationModel.IsAttachment && notificationModel.Attachments.Count > 0)
                {
                    foreach (var tm in notificationModel.Attachments)
                    {
                        UrlFileModel ms = tm.GetAttachment(notificationModel.Title);
                        mailMessage.Attachments.Add(new Attachment(tm, ms.contenType));
                    }
                }

                mailMessage.Body = notificationModel.Content;
                mailMessage.Subject = notificationModel.Title;
                mailMessage.IsBodyHtml = notificationModel.ContentType ==
                    EmailContentType.HTML ? true : false;

                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception x)
            {
                Log.Error(x, $"Send mail failed: {JsonConvert.SerializeObject(notificationModel)}");
                return false;
            }
        }
    }
}
