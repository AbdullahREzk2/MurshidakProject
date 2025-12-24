

namespace Project.BLL.Contracts
{
    public record ConfirmEmailRequest(
    string UserId,
    string Code
);

}
