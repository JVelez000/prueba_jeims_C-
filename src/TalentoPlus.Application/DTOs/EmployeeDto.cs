namespace TalentoPlus.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly HireDate { get; set; }
        public decimal Salary { get; set; }
        
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        
        public int JobTitleId { get; set; }
        public string JobTitleName { get; set; } = string.Empty;
        
        public int EducationLevelId { get; set; }
        public string EducationLevelName { get; set; } = string.Empty;
        
        public int EmploymentStatusId { get; set; }
        public string EmploymentStatusName { get; set; } = string.Empty;
        
        public string? ProfessionalProfile { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateEmployeeDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly HireDate { get; set; }
        public decimal Salary { get; set; }
        
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public int EducationLevelId { get; set; }
        public int EmploymentStatusId { get; set; }
        
        public string? ProfessionalProfile { get; set; }
        public string? Address { get; set; }
    }
}