namespace Project.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public EmailService(
            IWebHostEnvironment env,
            IConfiguration config,
            IEmailSender emailSender)
        {
            _env = env;
            _config = config;
            _emailSender = emailSender;
        }

        public async Task SendConfirmationEmailAsync(ApplicationUser user, string code)
        {
            var baseUrl = _config["AppUrl"]!.TrimEnd('/');
            var url = $"{baseUrl}/api/auth/confirm-email?userId={user.Id}&code={Uri.EscapeDataString(code)}";

            var body = new EmailBodyBuilder(_env)
                .GenerateEmailBody("EmailConfirmation", new()
                {
                    { "name", user.firstName },  
                    { "action_url", url }        
                });

            await _emailSender.SendEmailAsync(
                user.Email!,
                "Email Confirmation",
                body);
        }

        public async Task SendResetPasswordEmailAsync(ApplicationUser user, string code)
        {
            var baseUrl = _config["AppUrl"]!.TrimEnd('/');
            var url = $"{baseUrl}/password/reset-password.html?email={user.Email}&code={Uri.EscapeDataString(code)}";

            var body = new EmailBodyBuilder(_env)
                .GenerateEmailBody("ForgetPassword", new()
                {
                    { "name", user.firstName },  
                    { "action_url", url }        
                });

            await _emailSender.SendEmailAsync(
                user.Email!,
                "Reset Password",
                body);
        }


    }
}