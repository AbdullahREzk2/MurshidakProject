
namespace Project.BLL.IServices
{
    public interface IprofileService
    {
        Task<ProfileDto> getProfileByUserIdAsync(string userId,CancellationToken cancellationToken);
        Task<RegistrationProgressDto> GetRegistrationProgressAsync(string userId, CancellationToken cancellationToken);
        Task<string?> UploadProfileImageAsync(string userId,string fileName, CancellationToken ct);

    }
}
