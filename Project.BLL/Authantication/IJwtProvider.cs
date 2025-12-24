

using Project.DAL.Entities;

namespace Project.BLL.Authantication
{
    public interface IJwtProvider
    {
        (string token, int expiresIn) GenerateToken(ApplicationUser user);
        string? ValidateToken(string token);
    }
}
