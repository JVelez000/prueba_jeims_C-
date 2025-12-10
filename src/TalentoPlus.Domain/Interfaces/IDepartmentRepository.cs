using TalentoPlus.Domain.Entities;

namespace TalentoPlus.Domain.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllWithEmployeesAsync();
    }
}
