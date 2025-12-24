
namespace Project.BLL.Mapping
{
    public class ProfileMap:Profile
    {
        public ProfileMap()
        {
            CreateMap<ApplicationUser,ProfileDto>()
                .ForMember(d=>d.LevelName,opt=>opt.MapFrom(s=>s.Level!=null?s.Level.Name:null))
                .ForMember(d=>d.SpecializationName,opt=>opt.MapFrom(s=>s.Specialization!=null?s.Specialization.Name:null))
                .ReverseMap();
        }

    }
}
