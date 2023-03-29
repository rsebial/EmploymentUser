using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Infrastructure.MySQL;

namespace EmployeeUserApp.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(UserContext UserContext)
    {
        _repositories = new List<IRepository>();
        _UserContext = UserContext;
    }

    private readonly IList<IRepository> _repositories;
    private readonly UserContext _UserContext;

    public IAsyncRepository<T> AsyncRepository<T>() 
        where T : class, IEntity<long>
    {
        var repository = new GenericRepository<T>(_UserContext);
        _repositories.Add(repository);
        return repository;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _UserContext.SaveChangesAsync(cancellationToken);
    }
}
