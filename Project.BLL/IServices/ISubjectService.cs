
using Project.BLL.DTOS;

namespace Project.BLL.IServices
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetAllSubjectsAsync(CancellationToken cancellationToken);
        Task<SubjectDTO> GetSubjectByIdAsync(int subjectId,CancellationToken cancellationToken);
        Task<bool> EnrollSubjectAsync(EnrollSubjectUserDTO enrollSubject, string UserId ,CancellationToken cancellationToken);
        Task<IEnumerable<SubjectDTO>> GetEnrolledSubjectsAsync(string userId,CancellationToken cancellationToken);
    }
}
