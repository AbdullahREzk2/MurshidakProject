
namespace Project.DAL.IRepository
{
    public interface IProfileRepository
    {
        Task<ApplicationUser?> GetProfileByUserIdAsync(string userId,CancellationToken cancellationToken);
        Task UpdateProfilePictureAsync(string userId, string url, CancellationToken ct);

    }
}
