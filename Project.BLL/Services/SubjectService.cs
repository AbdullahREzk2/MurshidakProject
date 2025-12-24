

namespace Project.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(IMapper mapper , ISubjectRepository subjectRepository)
        {
            _mapper = mapper;
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllSubjectsAsync(CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetAllSubjectsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<SubjectDTO>>(subjects);
        }
        public async Task<SubjectDTO> GetSubjectByIdAsync(int subjectId, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(subjectId, cancellationToken);

            if (subject == null)
                return null!;

            return _mapper.Map<SubjectDTO>(subject);
        }
        public async Task<bool> EnrollSubjectAsync(EnrollSubjectUserDTO enrollSubject,string UserId ,CancellationToken cancellationToken)
        {
            return await _subjectRepository.EnrollSubjectAsync(enrollSubject.SubjectId,UserId, cancellationToken);
        }

        public async Task<IEnumerable<SubjectDTO>> GetEnrolledSubjectsAsync(string userId,CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetEnrolledSubjectsByUserIdAsync(userId, cancellationToken);

            return subjects
                .Select(x => _mapper.Map<SubjectDTO>(x.Subject))
                .ToList();
        }



    }
}
