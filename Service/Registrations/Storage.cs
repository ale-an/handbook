using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Abstractions;
using Storage.Logic;
using Storage.Logic.Repositories;

namespace Service.Registrations;

public static class Storage
{
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSingleton<IEmployeeRepository, EmployeeRepository>()
            .AddSingleton<IEmployeeGenerator, EmployeeGenerator>()
            .AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));
}