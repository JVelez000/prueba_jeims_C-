using Microsoft.EntityFrameworkCore;
using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Interfaces;
using TalentoPlus.Infrastructure.Data;
using System.Linq.Expressions;

namespace TalentoPlus.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task DeleteByIdAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM employees WHERE id = {0}", id);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .Include(e => e.EducationLevel)
                .Include(e => e.EmploymentStatus)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .Include(e => e.EducationLevel)
                .Include(e => e.EmploymentStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await _context.Employees
                .Where(predicate)
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .Include(e => e.EducationLevel)
                .Include(e => e.EmploymentStatus)
                .ToListAsync();
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(Employee entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            entity.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Employee entity)
        {
            _context.Employees.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task<Employee?> GetByDocumentAsync(string documentNumber)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.DocumentNumber == documentNumber);
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
        
        public async Task<int> GetCountByStatusNameAsync(string statusName)
        {
            var lowerName = statusName.ToLower();
    
            return await _context.Employees
                .Include(e => e.EmploymentStatus)
                .Where(e => e.EmploymentStatus.StatusName.ToLower().Contains(lowerName))
                .CountAsync();
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentAsync(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        {
            return await _context.Employees.Where(e => e.IsActive).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<int> GetCountByStatusAsync(int statusId)
        {
            return await _context.Employees.Where(e => e.EmploymentStatusId == statusId).CountAsync();
        }

        public async Task<int> GetCountByDepartmentAsync(int departmentId)
        {
            return await _context.Employees.Where(e => e.DepartmentId == departmentId).CountAsync();
        }
    }
}