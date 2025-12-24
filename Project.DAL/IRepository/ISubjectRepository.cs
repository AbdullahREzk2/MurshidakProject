
namespace Project.DAL.IRepository
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync(CancellationToken cancellationToken);
        Task<Subject?> GetSubjectByIdAsync(int id,CancellationToken cancellationToken);
        Task<bool>EnrollSubjectAsync(int subjectId, string userId,CancellationToken cancellationToken);
        Task<IEnumerable<UserSubject>> GetEnrolledSubjectsByUserIdAsync(string userId,CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
