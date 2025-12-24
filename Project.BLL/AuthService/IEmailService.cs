
namespace Project.BLL.AuthService
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(ApplicationUser user, string code);
        Task SendResetPasswordEmailAsync(ApplicationUser user, string code);
    }
}
