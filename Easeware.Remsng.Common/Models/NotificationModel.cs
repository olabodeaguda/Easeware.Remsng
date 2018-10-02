using Easeware.Remsng.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Common.Models
{
    public class NotificationModel
    {
        public NotificationModel()
        {
            ToEmails = new List<string>();
            Attachments = new List<string>();
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public string FromEmail { get; set; }
        public List<string> ToEmails { get; set; }
        public string Content { get; set; }
        public EmailContentType ContentType { get; set; } = EmailContentType.HTML;
        public bool IsAttachment { get; set; }
        public List<string> Attachments { get; set; }

    }
}
