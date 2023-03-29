namespace EmployeeUserApp.Domain.Models;

/// <summary>
///  simple classes, no methods or logic in here
/// </summary>
public class Employment : EntityBase
{
    public long Id { get; set; }
    public string? Company { get; set; }
    public uint? MonthsOfExperience { get; set; }
    public uint? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
