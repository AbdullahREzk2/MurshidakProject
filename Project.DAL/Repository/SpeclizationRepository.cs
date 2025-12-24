
namespace Project.DAL.Repository
{
    public class SpeclizationRepository : ISpeclizationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SpeclizationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync()
        {
            return await _dbContext.Specializations.ToListAsync();
        }


    }
}
