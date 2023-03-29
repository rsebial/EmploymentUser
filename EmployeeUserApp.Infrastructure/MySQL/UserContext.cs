using EmployeeUserApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeUserApp.Infrastructure.MySQL;

internal class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }

    public DbSet<User>? User { get; set; }
    public DbSet<Employment>? Employments { get; set; }
    public DbSet<Address>? Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    } 
}
