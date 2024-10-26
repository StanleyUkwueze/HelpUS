using System.Linq.Expressions;

namespace HelpUs.API.DataAccess.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<T> GetByIDAsync(object id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> SaveAsync();
        IQueryable<T> GetAll();
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveRangeAsync(IEnumerable<T> entity);
    }
}
