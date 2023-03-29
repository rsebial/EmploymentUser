using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Domain.Models;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeUserApp.Domain.Commands;

public static class UserCreate
{
    /// commands are immutable, so record (class) is the best type to use   
    /// commands don't contain model objects, or any other objects, but have to be considered as an instruction to alter the domain
    public record Command(string FirstName, string LastName, double Temperature, DateTime RecordDate) :IRequest<long>;

    public class Handler : IRequestHandler<Command, long>
    {
        public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Handler> _logger;

        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope($"More User will be created : {request.LastName}")) // -- log scope
            {
                
                using var repository = _unitOfWork.AsyncRepository<User>();
                var entity = request.Adapt<User>();
                
                await repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
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
