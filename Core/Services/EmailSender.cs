using Domain.Contracts;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class EmailSender(IConfiguration configuration) : IEmailSender
    {
        public async Task SendEmailAsync(Email email)
        {
            // Fetch settings from configuration
            var senderEmail = configuration["EmailSettings:Email"];
            var appPassword = configuration["EmailSettings:Password"];
            var host = configuration["EmailSettings:Host"];
            var port = int.Parse(configuration["EmailSettings:Port"]);

            // Set up the SMTP client
            using var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, appPassword)
            };

            // Create the mail message
            var mailMessage = new MailMessage(senderEmail, email.To, email.Subject, email.Body)
            {
                IsBodyHtml = false // Using basic text as requested
            };

            // Send the email asynchronously to prevent blocking the API
            await client.SendMailAsync(mailMessage);
        }
        
    }
}
