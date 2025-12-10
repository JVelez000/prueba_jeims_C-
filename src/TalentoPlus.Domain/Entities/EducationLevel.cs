namespace TalentoPlus.Domain.Entities
{
    public class EducationLevel : BaseEntity
    {
        public string LevelName { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
