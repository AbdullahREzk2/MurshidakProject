

namespace Project.DAL.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
