using Abstractions;
using Logic;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Registrations;

public static class Services
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddSingleton<IEmployeeService, EmployeeService>();
}