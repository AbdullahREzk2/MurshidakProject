
namespace Project.BLL.DTOS
{
    public class ProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UniId { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public string? LevelName { get; set; } 
        public string? SpecializationName { get; set; } 
    }

}
