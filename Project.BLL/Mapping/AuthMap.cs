namespace Project.BLL.Mapping
{
    public class AuthMap : Profile
    {
        public AuthMap()
        {
            CreateMap<RegisterRequest, ApplicationUser>(MemberList.None)
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UniId, opt => opt.MapFrom(src => src.UniId))
                .ForMember(dest => dest.LevelId, opt => opt.MapFrom(src=>src.LevelId))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src=>src.SpecializationId))
                .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore())
                .ForMember(dest => dest.UserSubjects, opt => opt.Ignore())
                .ForMember(dest => dest.Notifications, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.Level, opt => opt.Ignore())
                .ForMember(dest => dest.Specialization, opt => opt.Ignore());
        }
    }
}