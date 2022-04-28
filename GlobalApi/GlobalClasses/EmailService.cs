using GlobalApi.Models.Authentication;
using MailKit.Net.Smtp;
using MimeKit;

namespace GlobalApi.GlobalClasses
{
    public interface IEMailService
    {
        Task SendEmailAsync(string name, string toEmail, string subject, string content);
    }
    public class EmailService: IEMailService
    {
        private IConfiguration _configuration;
        EmailConfiguration _emailConfiguration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailConfiguration = new EmailConfiguration();
        }

        public async Task SendEmailAsync(string name, string toEmail, string subject, string content)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress("Telemedicine", _emailConfiguration.SmtpUsername);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(name, toEmail);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = subject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = content;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, _emailConfiguration.UseSSL);
                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                await emailClient.SendAsync(emailMessage);
                //emailClient.Disconnect(true);
            }
            catch (Exception ex)
            {
                //Log Exception Details
                throw new Exception(ex.Message);
            }
        }
    }
}
