
namespace Project.BLL.Services
{
    public class profileService : IprofileService
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;

        public profileService(IMapper mapper ,IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        public async Task<ProfileDto> getProfileByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
           var profile = await  _profileRepository.GetProfileByUserIdAsync(userId, cancellationToken);

            if (profile == null)
                return null!;

                return _mapper.Map<ProfileDto>(profile);
        }

        public async Task<RegistrationProgressDto> GetRegistrationProgressAsync(string userId,CancellationToken cancellationToken)
        {
            var user = await _profileRepository.GetProfileByUserIdAsync(userId, cancellationToken);

            var result = new RegistrationProgressDto();
            if (user == null)
                return result; 

            var steps = new List<(string Name, bool Completed, int Weight)>
            {
                ("Email Provided", !string.IsNullOrEmpty(user.Email), 10),
                ("Email Confirmed", user.EmailConfirmed, 30),
                ("Name Provided", !string.IsNullOrEmpty(user.firstName) && !string.IsNullOrEmpty(user.lastName), 10),
                ("Level Selected", user.LevelId.HasValue, 20),
                ("Specialization Selected", user.SpecializationId.HasValue, 20),
                ("Profile Picture Uploaded", !string.IsNullOrEmpty(user.ProfileImageUrl), 10) 
            };

            foreach (var step in steps)
            {
                if (step.Completed)
                {
                    result.Percentage += step.Weight;
                    result.CompletedSteps.Add(step.Name);
                }
            }

            return result;
        }

        public async Task<string?> UploadProfileImageAsync(string userId,string fileName, CancellationToken ct)
        {
            var url = $"/uploads/profile_images/{fileName}";

            await _profileRepository.UpdateProfilePictureAsync(userId, url, ct);

            return url;
        }


    }
}
