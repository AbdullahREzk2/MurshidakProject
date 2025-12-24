
namespace Project.BLL.IServices
{
    public interface ISpeclizationService
    {
        Task<IEnumerable<SpeclizationDTO>> GetAllSpecializationsAsync();
    }
}
