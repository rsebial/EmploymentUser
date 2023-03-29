namespace EmployeeUserApp.Domain.Interfaces;

public interface IUnitOfWork
{
    IAsyncRepository<TEntity> AsyncRepository<TEntity>() where TEntity : class, IEntity<long>;
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
