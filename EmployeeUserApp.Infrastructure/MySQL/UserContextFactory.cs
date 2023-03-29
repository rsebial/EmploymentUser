using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeUserApp.Infrastructure.MySQL;

internal class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
{
    public UserContext CreateDbContext(string[] args)
    {
        var optionsbuilder = new DbContextOptionsBuilder<UserContext>();
        var connectionString = "server=localhost;user=Employee_user;password=Employee123;database=EmployeeDb";
        optionsbuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new UserContext(optionsbuilder.Options);
    }
}
