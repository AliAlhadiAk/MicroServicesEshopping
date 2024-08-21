using IdentityService.Services.Interfaces;
using System.Net.Mail;
using System.Net;
using IdentityService.Model;

namespace IdentityService.Services.Repos
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.office365.com";
        private readonly int _smtpPort = 587;
      

        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, object body)
        {
            try
            {
                // Create SMTP client and configure credentials
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.EnableSsl = true; // Enable SSL/TLS
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Email.EmailSender, Email.EmailSenderPass);

                    // Create email message
                    using (var message = new MailMessage(Email.EmailSender, recipientEmail))
                    {
                        message.Subject = subject;
                        message.Body = body.ToString();
                        message.IsBodyHtml = false; 

                        // Send email asynchronously
                        await client.SendMailAsync(message);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email. Error message: {ex.Message}");
                return false;
            }
        }
    }
}
