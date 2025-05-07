using System.Linq.Expressions;

namespace LexHarvester.Persistence.UOW;

public interface IUnitOfWork<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> list);
    void Remove(T entity);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
}