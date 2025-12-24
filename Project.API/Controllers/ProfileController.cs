
namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IprofileService _profileService;
        private readonly IWebHostEnvironment _env;

        public ProfileController(IprofileService profileService,IWebHostEnvironment env)
        {
            _profileService = profileService;
            _env = env;
        }

        #region get user Profile
        [HttpGet("getProfile")]
        public async Task<IActionResult> getProfile(CancellationToken cancellationToken)
        {
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Invalid token" });

            var profile = await _profileService.getProfileByUserIdAsync(userId, cancellationToken);

            if (profile is null)
                return NotFound(new { Message = "Profile not found" });

            return Ok(profile);

        }
        #endregion

        #region get userProfile Progress
        [HttpGet("registration-progress")]
        public async Task<IActionResult> GetRegistrationProgress(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Invalid token" });

            var progress = await _profileService.GetRegistrationProgressAsync(userId, cancellationToken);

            return Ok(progress);
        }
        #endregion

        #region Upload Profile image
        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage([FromForm] UploadProfileImageDto dto, CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Invalid token" });

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest(new { Message = "No file uploaded" });

            // Generate unique file name
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";

            // Folder path in wwwroot/uploads/profile_images
            var folder = Path.Combine(_env.WebRootPath, "uploads", "profile_images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, fileName);

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream, ct);
            }

            var url = await _profileService.UploadProfileImageAsync(userId,fileName,ct);

            return Ok(new { ProfilePictureUrl = url });
        }
        #endregion

    }
}
