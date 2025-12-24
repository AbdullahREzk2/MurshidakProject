namespace Project.DAL.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
        public string? Description { get; set; }

        // FK (Doctor as Identity User)
        public string DoctorId { get; set; } = string.Empty;
        public ApplicationUser? Doctor { get; set; }

        // Enrollment
        public ICollection<UserSubject> UserSubjects { get; set; } = new List<UserSubject>();

        // 🔑 Prerequisites
        public ICollection<SubjectPrerequisite> Prerequisites { get; set; } = new List<SubjectPrerequisite>();
        public ICollection<SubjectPrerequisite> IsPrerequisiteFor { get; set; } = new List<SubjectPrerequisite>();
    }
}
