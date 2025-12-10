using TalentoPlus.Domain.Entities; // O donde estén tus interfaces de repositorio

namespace TalentoPlus.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Repositorios específicos o genéricos
        IEmployeeRepository Employees { get; }
        
        // Necesitas agregar estos si no existen:
        IRepository<Department> Departments { get; }
        IRepository<JobTitle> JobTitles { get; }
        IRepository<EducationLevel> EducationLevels { get; }
        IRepository<EmploymentStatus> EmploymentStatuses { get; }
        
        Task<int> CompleteAsync();
    }
}