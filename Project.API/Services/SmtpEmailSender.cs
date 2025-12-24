
namespace Project.API.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailSettings = _config.GetSection("MailSettings");

            string host = mailSettings["Host"]!;
            int port = mailSettings.GetValue<int>("Port");
            string username = mailSettings["Username"]!;
            string password = mailSettings["Password"]!;
            string fromEmail = mailSettings["FromEmail"]!;
            string fromName = mailSettings["FromName"]!;

            using var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            using var message = new MailMessage()
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(email);

            await smtp.SendMailAsync(message);
        }
    }
}
