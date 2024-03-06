using Evaluation.DAL;
using Evaluation.DAL.Contracts;
using Evaluation.DAL.Repositories;
using Evaluation.Services;
using Evaluation.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<DbContextEntity>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("EvalAPIConnectionString"));
        });
        services.AddScoped<IEvenementRepository, EvenementRepository>();
        services.AddScoped<IEvenementService, EvenementService>();

    })
    .Build();

//Seed the databse
using (IServiceScope scope = host.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;

    var dbContext = serviceProvider.GetService<DbContextEntity>();
    dbContext.Database.EnsureCreated();
}

host.Run();
