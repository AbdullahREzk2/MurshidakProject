
namespace Project.BLL.DTOS
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string? Description { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public List<string> Prerequisites { get; set; } = new();
        public List<string> IsPrerequisiteFor { get; set; } = new();


    }
}
