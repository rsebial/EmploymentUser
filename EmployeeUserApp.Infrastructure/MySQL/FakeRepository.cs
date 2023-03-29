using EmployeeUserApp.Domain.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeeUserApp.Infrastructure.MySQL;


internal class FakeRepository<T> : IAsyncRepository<T>
    where T : class, IEntity<long>
{
    public FakeRepository()
    {
        
    }

    private static IList<T> _data = new List<T>();

    ~FakeRepository()
    {
        Dispose(false);
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.Id = _data.Count + 1;
        _data.Add(entity);
        return await Task.FromResult(entity);
    }

    public async Task<T?> GetAsync(long id, params Expression<Func<T, object>>[] includes)
    {
        return await Task.FromResult(_data.SingleOrDefault(x => x.Id == id));
    }

    public long GetId(T entity)
    {
        return entity.Id;
    }

    public async Task<IEnumerable<T>> QueryAsync(Func<IQueryable<T>, IQueryable<T>>? filter = null, params Expression<Func<T, object>>[] includes)
    {
        if (filter != null)
            return await Task.FromResult(filter(_data.AsQueryable<T>()).ToList());
        else
            return await Task.FromResult(_data.AsEnumerable());
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var entity = _data.SingleOrDefault(x => x.Id == id);
        if (entity != null)
        {
            _data.Remove(entity);
            return true;
        }
        await Task.Yield();
        return false;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await Task.Yield();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var existing = _data.SingleOrDefault(x => x.Id == entity.Id);
        if(existing != null)
        {
            var index = _data.IndexOf(existing);
            _data[index] = entity;
        }        
        return await Task.FromResult(true);
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {

        }
    }
}
