using GlobalApi.Models.Authentication;
//using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System.Net;
//using MimeKit;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace GlobalApi.GlobalClasses
{
    public interface IEMailService
    {
        
        Task<string> SendEmailAsync(string name, string toEmail, string subject, string content);
        Task<string> SendStuEmailAsync(string name, string toEmail, string subject, string contents);
        Task<string> SendRegistrationEmailAsync_New(string name, string toEmail, string subject, string content);
        Task<string> SendEmailAsync_support(string name, string toEmail, string subject, string content);

    }
    public class EmailService : IEMailService
    {
        private IConfiguration _configuration;
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(IConfiguration configuration, IOptions<EmailConfiguration> mailSettings)
        {
            this._configuration = configuration;
            _emailConfiguration = mailSettings.Value;
        }

        public async Task<string> SendEmailAsync(string name, string toEmail, string subject, string content)
        {
            try
            {
                // Create a new MailMessage object
                MailMessage msg = new MailMessage(name, toEmail);
                msg.Subject = subject;
                msg.Body = content;
                msg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    // Set the SMTP server and port from the configuration
                    // string SmtpServer_Name = _configuration["EmailSettings:SmtpServer"];
                    smtp.Host = this._configuration["EmailSettings:SmtpServer"];
                    smtp.Port = int.Parse(this._configuration["EmailSettings:SmtpPort"]);
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;

                    // Create and set the network credentials
                    NetworkCredential NetworkCred = new NetworkCredential
                    {
                        UserName = this._configuration["EmailSettings:SmtpUsername"],
                        Password = this._configuration["EmailSettings:SmtpPassword"]
                    };
                    smtp.Credentials = NetworkCred;

                    // Bypass SSL certificate validation (for development/testing only; not recommended for production)
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    // Send the email asynchronously

                    await smtp.SendMailAsync(msg);
                    return "Mail sent successfully";
                }
            }
            catch (Exception ex)
            {
                // Log the exception details and rethrow the exception
                throw new Exception($"Error sending email: {ex.Message}", ex);
            }
        }
        public async Task<string> SendStuEmailAsync(string name, string toEmail, string subject, string contents)
        {
            try
            {
                // Create a new MailMessage object
                MailMessage msg = new MailMessage(name, toEmail);
                msg.Subject = subject;
                msg.Body = contents;
                msg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    // Set the SMTP server and port from the configuration
                    // string SmtpServer_Name = _configuration["EmailSettings:SmtpServer"];
                    smtp.Host = this._configuration["EmailSettings:SmtpServer"];
                    smtp.Port = int.Parse(this._configuration["EmailSettings:SmtpPort"]);
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;

                    // Create and set the network credentials
                    NetworkCredential NetworkCred = new NetworkCredential
                    {
                        UserName = this._configuration["EmailSettings:SmtpUsername"],
                        Password = this._configuration["EmailSettings:SmtpPassword"]
                    };
                    smtp.Credentials = NetworkCred;

                    // Bypass SSL certificate validation (for development/testing only; not recommended for production)
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    // Send the email asynchronously

                    await smtp.SendMailAsync(msg);
                    return "Mail sent successfully";
                }
            }
            catch (Exception ex)
            {
                // Log the exception details and rethrow the exception
                throw new Exception($"Error sending email: {ex.Message}", ex);
            }
        }

        public async Task<string> SendRegistrationEmailAsync_New(string name, string toEmail, string subject, string content)
        {
            try
            {
                // Create a new MailMessage object
                MailMessage msg = new MailMessage(name, toEmail);
                msg.Subject = subject;
                msg.Body = content;
                msg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    // Set the SMTP server and port from the configuration
                    // string SmtpServer_Name = _configuration["EmailSettings:SmtpServer"];
                    smtp.Host = this._configuration["EmailSettings:SmtpServer"];
                    smtp.Port = int.Parse(this._configuration["EmailSettings:SmtpPort"]);
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;

                    // Create and set the network credentials
                    NetworkCredential NetworkCred = new NetworkCredential
                    {
                        UserName = this._configuration["EmailSettings:SmtpUsername"],
                        Password = this._configuration["EmailSettings:SmtpPassword"]
                    };
                    smtp.Credentials = NetworkCred;

                    // Bypass SSL certificate validation (for development/testing only; not recommended for production)
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    // Send the email asynchronously

                    await smtp.SendMailAsync(msg);
                    return "Mail sent successfully";
                }
            }
            catch (Exception ex)
            {
                // Log the exception details and rethrow the exception
                throw new Exception($"Error sending email: {ex.Message}", ex);
            }
        }

        //Email notification for support ALDA, please do not change
        public async Task<string> SendEmailAsync_support(string name, string toEmail, string subject, string content)
        {
            try
            {
                //test ALDA
                //MailMessage msg = new MailMessage(name, "laksman.alda@vikasglobal.net");

                //live ALDA
                MailMessage msg = new MailMessage(name, "enquiry@neurospineoptica.com");

                msg.Subject = subject;
                msg.Body = content;
                msg.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {

                    smtp.Host = this._configuration["EmailSettings:SmtpServer"];
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;

                    NetworkCredential NetworkCred = new NetworkCredential();
                    NetworkCred.UserName = this._configuration["EmailSettings:SmtpUsername"];
                    NetworkCred.Password = this._configuration["EmailSettings:SmtpPassword"];
                    smtp.Credentials = NetworkCred;

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    await smtp.SendMailAsync(msg);
                    return "Mail sent successfully";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        
    }
}
