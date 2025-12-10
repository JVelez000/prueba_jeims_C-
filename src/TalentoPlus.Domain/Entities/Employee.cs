using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly HireDate { get; set; }
        public decimal Salary { get; set; }
        
        // Foreign keys
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public int EducationLevelId { get; set; }
        public int EmploymentStatusId { get; set; }
        
        // Navigation properties
        public Department? Department { get; set; }
        public JobTitle? JobTitle { get; set; }
        public EducationLevel? EducationLevel { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }
        
        // Additional info
        public string? ProfessionalProfile { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
