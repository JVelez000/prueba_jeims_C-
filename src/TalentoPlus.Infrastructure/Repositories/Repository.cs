using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TalentoPlus.Domain.Entities;
using TalentoPlus.Domain.Interfaces;
using TalentoPlus.Infrastructure.Data;

namespace TalentoPlus.Infrastructure.Repositories
{
    // CAMBIO IMPORTANTE: 'where T : BaseEntity' para poder acceder a 'Id' en ExistsAsync
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        // IMPLEMENTACIÓN FALTANTE 1: FindAsync
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        // CORRECCIÓN: Debe retornar T, no void
        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            // En UnitOfWork el SaveChanges se hace al final, pero retornamos la entidad
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            // Actualizamos la fecha de modificación automáticamente
            entity.UpdatedAt = DateTime.UtcNow; 
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        // IMPLEMENTACIÓN FALTANTE 2: ExistsAsync
        public async Task<bool> ExistsAsync(int id)
        {
            // Gracias a 'where T : BaseEntity', podemos acceder a .Id
            return await dbSet.AnyAsync(e => e.Id == id);
        }
    }
}