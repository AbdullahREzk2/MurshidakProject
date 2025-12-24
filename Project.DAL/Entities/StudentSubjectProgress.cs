namespace Project.DAL.Entities
{
    public class StudentSubjectProgress
    {
        public int Id { get; set; }

        // FK → Identity User (Student)
        public string StudentId { get; set; } = string.Empty;
        public ApplicationUser Student { get; set; } = null!;

        // FK → Subject
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        // Progress info
        public bool IsCompleted { get; set; } = false;

        public decimal? Grade { get; set; }     // e.g. 85.5
        public DateTime? CompletedAt { get; set; }
    }
}
