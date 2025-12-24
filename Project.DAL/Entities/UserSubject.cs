

namespace Project.DAL.Entities
{
    public class UserSubject
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public int SubjectId { get; set; }

        public DateTime RegisteredAt { get; set; }

        public ApplicationUser? User { get; set; }
        public Subject? Subject { get; set; }
    }
}
