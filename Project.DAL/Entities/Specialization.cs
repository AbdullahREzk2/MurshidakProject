

namespace Project.DAL.Entities
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
