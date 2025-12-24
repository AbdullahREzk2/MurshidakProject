
namespace Project.DAL.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string UniId { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }

        public ICollection<refreshToken> RefreshTokens { get; set; } = [];

        public int? LevelId { get; set; }
        public Level? Level { get; set; }

        public int? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }

        public ICollection<UserSubject>? UserSubjects { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
