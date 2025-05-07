
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace LexHarvester.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public Repository(ApplicationDbContext context) {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<T> AddAsync(T entity) {
        await _dbSet.AddAsync(entity);
        return entity;
    }
    public Task AddRangeAsync(IEnumerable<T> list) => _dbSet.AddRangeAsync(list);
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public void Remove(T entity) => _dbSet.Remove(entity);

    public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate) => _dbSet.SingleOrDefaultAsync(predicate);

    public Task<T> Update(T entity) {
        return Task.FromResult(_dbSet.Update(entity).Entity);
    }

    public Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate) =>  _dbSet.Where(predicate).ToListAsync();
}