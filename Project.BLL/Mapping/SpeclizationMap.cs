
namespace Project.BLL.Mapping
{
    public class SpeclizationMap:Profile
    {
        public SpeclizationMap()
        {
           CreateMap<Specialization, SpeclizationDTO>().ReverseMap();
        }

    }
}
