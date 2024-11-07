using Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Registrations;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builderContext, services) =>
    {
        services.AddStorage(builderContext.Configuration)
            .AddServices()
            .AddSingleton<ClientService>();
    })
    .Build();

var clientService = host.Services.GetRequiredService<ClientService>();

clientService.Execute();