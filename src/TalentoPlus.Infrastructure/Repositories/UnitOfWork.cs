using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Interfaces;
using TalentoPlus.Infrastructure.Data;

namespace TalentoPlus.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // Variables privadas de respaldo (Backing fields)
        private IEmployeeRepository? _employees;
        private IRepository<Department>? _departments;
        private IRepository<JobTitle>? _jobTitles;
        private IRepository<EducationLevel>? _educationLevels;
        private IRepository<EmploymentStatus>? _employmentStatuses;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementación de Employees (Usando su repositorio específico)
        public IEmployeeRepository Employees => 
            _employees ??= new EmployeeRepository(_context);

        // Implementación de Departments (Si DepartmentRepository hereda de Repository<Department>, esto funciona)
        // Si no tienes lógica especial en Departments, usa Repository<Department>
        // Si tienes DepartmentRepository, úsalo: new DepartmentRepository(_context)
        public IRepository<Department> Departments => 
            _departments ??= new DepartmentRepository(_context); 

        // Implementación de las tablas catálogo (Usando el Genérico Repository<T>)
        public IRepository<JobTitle> JobTitles => 
            _jobTitles ??= new Repository<JobTitle>(_context);

        public IRepository<EducationLevel> EducationLevels => 
            _educationLevels ??= new Repository<EducationLevel>(_context);

        public IRepository<EmploymentStatus> EmploymentStatuses => 
            _employmentStatuses ??= new Repository<EmploymentStatus>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}