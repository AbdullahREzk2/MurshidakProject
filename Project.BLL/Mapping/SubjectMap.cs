
public class SubjectMap : Profile
{
    public SubjectMap()
    {
        CreateMap<Subject, SubjectDTO>()
            .ForMember(dest => dest.DoctorName,
                       opt => opt.MapFrom(src => src.Doctor != null
                                                 ? src.Doctor.firstName + " " + src.Doctor.lastName
                                                 : "Unknown"))
            .ForMember(dest => dest.Prerequisites,
                       opt => opt.MapFrom(src => src.Prerequisites != null
                                                 ? src.Prerequisites.Select(p => p.Prerequisite.Name).ToList()
                                                 : new List<string>()))
            .ForMember(dest => dest.IsPrerequisiteFor,
                       opt => opt.MapFrom(src => src.IsPrerequisiteFor != null
                                                 ? src.IsPrerequisiteFor.Select(p => p.Subject.Name).ToList()
                                                 : new List<string>()));
    }
}
