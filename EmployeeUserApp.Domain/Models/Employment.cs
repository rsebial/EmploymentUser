namespace EmployeeUserApp.Domain.Models;

/// <summary>
///  simple classes, no methods or logic in here
/// </summary>
public class Address : EntityBase
{
    public int Id { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public int? PostCode { get; set; }
}
