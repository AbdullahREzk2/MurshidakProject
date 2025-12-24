
namespace Project.BLL.DTOS
{
    public class RegistrationProgressDto
    {
        public int Percentage { get; set; }
        public List<string> CompletedSteps { get; set; } = new();
    }
}
