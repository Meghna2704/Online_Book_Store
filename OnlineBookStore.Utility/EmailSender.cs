using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendgridSecret { get; set; }

        public EmailSender(IConfiguration _config)
        {
            SendgridSecret = _config.GetValue<string>("SendGrid:SecretKey");
            
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(SendgridSecret);

            var from = new EmailAddress("meghnav274@gmail.com", "OnlineBookStore");
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            var emailSent = await client.SendEmailAsync(message);
           // return emailSent;
        }
    }
}
