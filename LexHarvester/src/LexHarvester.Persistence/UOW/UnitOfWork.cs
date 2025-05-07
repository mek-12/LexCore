using System.Linq.Expressions;
using LexHarvester.Persistence.Repositories;

namespace LexHarvester.Persistence.UOW;
public class UnitOfWork<T> : IUnitOfWork<T> where T : class
{
    private IRepository<T> _repository;
    public UnitOfWork(IRepository<T> repository){
        _repository = repository;
    }
    public Task AddAsync(T entity) => _repository.AddAsync(entity);

    public Task AddRangeAsync(IEnumerable<T> list) => _repository.AddRangeAsync(list);

    public Task<IEnumerable<T>> GetAllAsync() => _repository.GetAllAsync();

    public void Remove(T entity) => _repository.Remove(entity);

    public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate) => _repository.SingleOrDefaultAsync(predicate);

    public Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate) => _repository.WhereAsync(predicate);
}