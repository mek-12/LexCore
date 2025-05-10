using System.Linq.Expressions;

namespace LexHarvester.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> list);
    void Remove(T entity);
    Task<T> Update(T entity);
}