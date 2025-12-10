using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Domain.Entities
{
    public class JobTitle : BaseEntity
    {
        // El Id, CreatedAt y UpdatedAt ya vienen de BaseEntity, NO los pongas aquí.
        
        public string TitleName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}