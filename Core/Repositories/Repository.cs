using Microsoft.EntityFrameworkCore;
using TastyByThreeMusketeers.Core.Interfaces;
using TastyByThreeMusketeers.Infrastructure.Data;

namespace TastyByThreeMusketeers.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CanteenDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(CanteenDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
        public async Task<T> GetById(Guid id) => await _dbSet.FindAsync(id);
        public async Task Add(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
