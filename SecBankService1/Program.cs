using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SecBank.WorkerService;
using System.Data.Common;




IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostService, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostService, config) =>
    {
        IConfiguration configuration = hostService.Configuration;
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        config.AddSingleton<DbConnection>(new SqliteConnection(connectionString));
        config.AddDbContextPool<ServiceDbContext>((serviceProvider, options) =>
        {
            options.UseSqlite(serviceProvider.GetRequiredService<DbConnection>().ConnectionString);
        });
        config.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();  