using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByDocumentAsync(string documentNumber);
        Task<Employee?> GetByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetByDepartmentAsync(int departmentId);
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task<int> GetTotalCountAsync();
        Task<int> GetCountByStatusAsync(int statusId);
        Task<int> GetCountByDepartmentAsync(int departmentId);
        
        Task DeleteByIdAsync(int id);
        
        Task<int> GetCountByStatusNameAsync(string statusName);
    }
}
