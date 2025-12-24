
namespace Project.BLL.Mapping
{
    public class levelMap:Profile
    {
        public levelMap()
        {
            CreateMap<Level, LevelDTO>().ReverseMap();
        }
    }
}
