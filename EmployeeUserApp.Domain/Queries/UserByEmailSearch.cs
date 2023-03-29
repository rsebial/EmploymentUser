using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Domain.Models;
using EmployeeUserApp.Domain.Queries.Filters;
using MediatR;

namespace EmployeeUserApp.Domain.Queries;

public static class UserByEmailSearch
{
    public record Query(string? Email) : IRequest<IEnumerable<User>>;

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
            using var repository = _unitOfWork.AsyncRepository<User>(); // important : using!

            // construct filter
            var filter = (IQueryable<User> q) => q
                .WhereEmail(request.Email);

            // query and return
            var result = await repository.QueryAsync(filter);
            return result;
        }
    }
}
