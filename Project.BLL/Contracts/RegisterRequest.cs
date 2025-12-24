
namespace Project.BLL.Contracts
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UniId { get; set; } = string.Empty;
        public int LevelId { get; set; }
        public int SpecializationId { get; set; }
    }
}
