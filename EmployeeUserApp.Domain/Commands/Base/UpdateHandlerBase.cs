using EmployeeUserApp.Domain.Interfaces;
using Mapster;

namespace EmployeeUserApp.Domain.Commands.Base;

public abstract class UpdateHandlerBase<TCommand, TEntity>
    where TEntity : class, IEntity<long>
{
    protected UpdateHandlerBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    protected virtual async Task<UpdateResult> Handle(long id, TCommand request, CancellationToken cancellationToken)
    {
        using var repository = _unitOfWork.AsyncRepository<TEntity>();
        var existing = await repository.GetAsync(id);
        if (existing == null)
            return UpdateResult.NotFound;

        var entity = request.Adapt<TEntity>();
        var success = await repository.UpdateAsync(entity);
        if (success)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return UpdateResult.Success;
    }
}
