using Microsoft.EntityFrameworkCore;
using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Interfaces;
using TalentoPlus.Infrastructure.Data;

namespace TalentoPlus.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<IEnumerable<Department>> FindAsync(System.Linq.Expressions.Expression<Func<Department, bool>> predicate)
        {
            return await _context.Departments.Where(predicate).ToListAsync();
        }

        public async Task<Department> AddAsync(Department entity)
        {
            await _context.Departments.AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(Department entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            entity.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Department entity)
        {
            _context.Departments.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> GetAllWithEmployeesAsync()
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .ToListAsync();
        }
    }
}
