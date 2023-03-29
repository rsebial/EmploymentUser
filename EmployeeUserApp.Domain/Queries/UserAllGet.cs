using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Domain.Models;
using MediatR;

namespace EmployeeUserApp.Domain.Queries;

public static class UserAllGet // static class, containing only other classes
{

    // queries are immutable, so record (class) is the best type to use
    public record Query() : IRequest<IEnumerable<User>>;

    // Handler receive specific query and return domain models (or list of domain models)
    public class Handler : IRequestHandler<Query, IEnumerable<User>>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<IEnumerable<User>> Handle(Query request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<User>();

            var entity = await repository.QueryAsync();
            return entity;
        }
    }
}
