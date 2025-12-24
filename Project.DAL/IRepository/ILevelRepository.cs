
namespace Project.DAL.IRepository
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Level>> GetAllLevelsAsync();
    }
}
