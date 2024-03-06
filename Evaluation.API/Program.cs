using Evaluation.DAL;
using Evaluation.DAL.Contracts;
using Evaluation.Services;
using Evaluation.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IService, Service>();

    })
    .Build();

host.Run();
