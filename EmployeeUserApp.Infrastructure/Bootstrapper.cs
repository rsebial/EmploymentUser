using EmployeeUserApp.Domain.Interfaces;
using EmployeeUserApp.Infrastructure.MySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeUserApp.Infrastructure;

public static class Bootstrapper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        var connectionString = configuration.GetConnectionString("UserContext");

        services.AddDbContext<UserContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


        return services;
    }
}