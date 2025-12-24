namespace Project.DAL.Entities
{
    public class SubjectPrerequisite
    {
        public int SubjectId { get; set; }        // المادة اللي عايزين نسجلها
        public Subject Subject { get; set; } = null!;

        public int PrerequisiteId { get; set; }   // المادة المطلوبة قبلها
        public Subject Prerequisite { get; set; } = null!;
    }
}
