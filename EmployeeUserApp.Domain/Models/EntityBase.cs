using EmployeeUserApp.Domain.Interfaces;

namespace EmployeeUserApp.Domain.Models;

public abstract class EntityBase : IEntity<long>
{
    public long Id { get; set; }
}
