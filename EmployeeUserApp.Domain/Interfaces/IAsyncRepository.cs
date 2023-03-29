using System.Linq.Expressions;

namespace EmployeeUserApp.Domain.Interfaces;

public interface IAsyncRepository<TEntity> : IRepository, IDisposable
    where TEntity : class,IEntity<long>
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> RemoveAsync(long id);
    Task<TEntity?> GetAsync(long id, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> QueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null, params Expression<Func<TEntity, object>>[] includes);
    long GetId(TEntity entity);
}