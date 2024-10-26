using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace HelpUs.API.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        internal DbSet<T> dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public async Task<bool> AddAsync(T entity)
        {
            dbSet.Add(entity);
            return await SaveAsync();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = dbSet.AsQueryable();
            return query;
        }

        public async Task<T> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<T> entity)
        {
            dbSet.RemoveRange();
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            return await SaveAsync();
        }
    }
}
