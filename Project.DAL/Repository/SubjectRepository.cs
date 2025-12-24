
namespace Project.DAL.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync(CancellationToken cancellationToken)
        {
            return await _context.Subjects
                .Include(s => s.Doctor)
                .Include(s => s.Prerequisites)
                    .ThenInclude(sp => sp.Prerequisite)
                .Include(s => s.IsPrerequisiteFor)
                    .ThenInclude(sp => sp.Subject)
                .Where(s => s.IsAvailable)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Subjects
                .Include(s => s.Doctor)
                .Include(s => s.Prerequisites)
                    .ThenInclude(sp => sp.Prerequisite)
                .Include(s => s.IsPrerequisiteFor)
                    .ThenInclude(sp => sp.Subject)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<bool> EnrollSubjectAsync(int subjectId,string userId,CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects
                .Include(s => s.Prerequisites)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == subjectId && s.IsAvailable, cancellationToken);

            if (subject == null)
                return false;

            bool alreadyEnrolled = await _context.UserSubjects
                .AnyAsync(us => us.SubjectId == subjectId && us.UserId == userId, cancellationToken);

            if (alreadyEnrolled)
                return false;

            var prerequisiteIds = subject.Prerequisites
                .Select(p => p.PrerequisiteId)
                .ToList();

            if (prerequisiteIds.Any())
            {
                var completedSubjectIds = await _context.StudentSubjectProgress
                    .Where(p => p.StudentId == userId && p.IsCompleted)
                    .Select(p => p.SubjectId)
                    .ToListAsync(cancellationToken);

                var missingPrerequisites = prerequisiteIds
                    .Except(completedSubjectIds)
                    .ToList();

                if (missingPrerequisites.Any())
                    return false; 
            }

            _context.UserSubjects.Add(new UserSubject
            {
                UserId = userId,
                SubjectId = subjectId,
                RegisteredAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<UserSubject>> GetEnrolledSubjectsByUserIdAsync(string userId,CancellationToken cancellation = default)
        {
            return await _context.UserSubjects
                .Where(x => x.UserId == userId)
                .Include(x => x.Subject)
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

      
    }
}
