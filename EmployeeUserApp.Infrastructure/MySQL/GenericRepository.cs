using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Domain.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeUserApp.Infrastructure.MySQL;


internal class GenericRepository<T> : IAsyncRepository<T>
    where T : class, IEntity<long>
{
    public GenericRepository(UserContext EmployeeContext)
    {
        _UserContext = EmployeeContext;
    }

    private readonly UserContext _UserContext;

    ~GenericRepository()
    {
        Dispose(false);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _UserContext.AddAsync(entity);
        return entity;
    }

    public async Task<T?> GetAsync(long id, params Expression<Func<T,object>>[] includes)
    {
        var query = _UserContext.Set<T>().AsNoTracking();
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public long GetId(T entity)
    {
        return entity.Id;
    }

    public async Task<IEnumerable<T>> QueryAsync(Func<IQueryable<T>,  IQueryable<T>>? filter = null, params Expression<Func<T, object>>[] includes)
    {
        var query = _UserContext.Set<T>().AsNoTracking();
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (filter != null)
            return await Task.FromResult(filter(query).ToList());
        else
            return await Task.FromResult(query.AsEnumerable());
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var entity = _UserContext.Find<T>(id);
        if (entity != null)
        {
            _UserContext.Remove(entity);
            return true;
        }
        await Task.Yield();
        return false;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _UserContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _UserContext.Update(entity);
        return await Task.FromResult(true);
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        //if (disposing)
        //{

        //}
    }
}
