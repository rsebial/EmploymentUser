namespace EmployeeUserApp.Domain.Models;

/// <summary>
///  simple classes, no methods or logic in here
/// </summary>
public class User : EntityBase
{
    public User() { 
        Employments = new List<Employment>();
    }
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public Address? Address { get; set; }
    public List<Employment> Employments { get; set; }
}
