namespace TalentoPlus.Domain.Entities
{
    public class EmploymentStatus : BaseEntity
    {
        public string StatusName { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
