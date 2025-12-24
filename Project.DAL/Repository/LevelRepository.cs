
namespace Project.DAL.Repository
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LevelRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Level>> GetAllLevelsAsync()
        {
            return await _dbContext.Levels.ToListAsync();
        }


    }
}
