namespace EmployeeUserApp.Domain.Interfaces;

public interface IRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}