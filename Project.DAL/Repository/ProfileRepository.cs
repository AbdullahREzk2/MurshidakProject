
namespace Project.DAL.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser?> GetProfileByUserIdAsync(string userId,CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Level)
                .Include(u => u.Specialization)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task UpdateProfilePictureAsync(string userId, string url, CancellationToken ct)
        {
            var user = await _dbContext.Users.FindAsync(new object[] { userId }, ct);
            if (user == null) return;

            user.ProfileImageUrl = url;
            await _dbContext.SaveChangesAsync(ct);
        }


    }

}
