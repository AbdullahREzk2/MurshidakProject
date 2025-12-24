
namespace Project.DAL.IRepository
{
    public interface ISpeclizationRepository
    {
        Task<IEnumerable<Specialization>> GetAllSpecializationsAsync();
    }
}
