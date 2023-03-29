using EmployeeUserApp.Domain.Commands.Base;
using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Domain.Models;
using Mapster;
using MediatR;

namespace EmployeeUserApp.Domain.Commands;

public static class UserUpdate
{
    public record Command(int Id, string FirstName, string LastName, double Temperature, DateTime RecordDate) :IRequest<UpdateResult>;

    public class Handler : UpdateHandlerBase<Command, User>, IRequestHandler<Command, UpdateResult>
    {
        public Handler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UpdateResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return base.Handle(request.Id, request, cancellationToken);
        }
    }

    public class Map : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Command, User>();
        }
    }
}
