
namespace Project.BLL.IServices
{
    public interface ILevelService
    {
        Task<IEnumerable<LevelDTO>> GetAllLevelsAsync();
    }
}
