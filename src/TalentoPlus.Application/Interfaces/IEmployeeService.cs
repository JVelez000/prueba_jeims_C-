using TalentoPlus.Application.DTOs;

namespace TalentoPlus.Application.Interfaces
{
    public interface IEmployeeService
    {
        // CAMBIA ESTA LÍNEA (Agrega el parámetro opcional)
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string? searchTerm = null);

        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto employeeDto);
        Task UpdateEmployeeAsync(int id, CreateEmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int id);
        
        // Métodos de conteo (Dashboard)
        Task<int> GetTotalEmployeesCountAsync();
        Task<int> GetEmployeesByStatusCountAsync(int statusId);
        Task<int> GetEmployeesByDepartmentCountAsync(int departmentId);

        // Métodos para Dropdowns
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<IEnumerable<JobTitleDto>> GetJobTitlesAsync();
        Task<IEnumerable<EducationLevelDto>> GetEducationLevelsAsync();
        Task<IEnumerable<EmploymentStatusDto>> GetEmploymentStatusesAsync();
        
        Task<int> GetEmployeesByStatusNameCountAsync(string statusName);
    }
}