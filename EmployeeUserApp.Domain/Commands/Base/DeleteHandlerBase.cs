using EmployeeUserApp.Domain.Interfaces;

namespace EmployeeUserApp.Domain.Commands.Base;

public abstract class DeleteHandlerBase<TEntity>
    where TEntity : class, IEntity<long>
{
    protected DeleteHandlerBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    protected virtual async Task<DeleteResult> Handle(long id, CancellationToken cancellationToken)
    {
        using var repository = _unitOfWork.AsyncRepository<TEntity>();

        var exists = await repository.GetAsync(id);
        if (exists == null) return DeleteResult.NotFound;

        var success = await repository.RemoveAsync(id);

        if (success)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return DeleteResult.Success;
        }
        return DeleteResult.Forbidden;
    }
}
