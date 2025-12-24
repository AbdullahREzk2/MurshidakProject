
namespace Project.BLL.Contracts
{
    public record ResetPasswordRequest(
    string Email,
    string Code,
    string NewPassword
);

}
