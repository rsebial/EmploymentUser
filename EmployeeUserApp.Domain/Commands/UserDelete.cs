using EmployeeUserApp.Domain.Commands.Base;
using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Domain.Models;
using EmployeeUserApp.Domain.Queries.Filters;
using MediatR;

namespace EmployeeUserApp.Domain.Commands;

public static class UserDelete
{
    public record Command(int Id) :IRequest<DeleteResult>;

    public class Handler : DeleteHandlerBase<User>, IRequestHandler<Command, DeleteResult>
    {
        public Handler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<DeleteResult> Handle(Command request, CancellationToken cancellationToken)
        {
            
            return await base.Handle(request.Id, cancellationToken);
        }

    } 
}
