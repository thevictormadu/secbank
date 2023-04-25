using Microsoft.EntityFrameworkCore;
using SecBank.BgService;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"C:\temp\SecBankWorkerService\LogFile.txt")
    .CreateLogger();


try
{
    Log.Information("Starting up the service");

    IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IService, Service>();
                services.AddDbContext<ServiceDbContext>(options =>
                { options.UseSqlite(hostContext.Configuration.GetConnectionString("DefaultDbConnection")); }, ServiceLifetime.Singleton);
                services.AddHostedService<Worker>();
            }).UseSerilog().Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "There was a problem starting this service");
    return;
}
finally
{
    Log.CloseAndFlush();
}

//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureAppConfiguration((hostService, config) =>
//    {
//        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//        config.AddEnvironmentVariables();
//    })
//    .ConfigureServices((hostService, config) =>
//    {
//        IConfiguration configuration = hostService.Configuration;
//        string connectionString = configuration.GetConnectionString("DefaultConnection");
//        config.AddSingleton<DbConnection>(new SqliteConnection(connectionString));
//        config.AddSingleton<IService, Service>();
//        config.AddDbContext<ServiceDbContext>((serviceProvider, options) =>
//        {
//            options.UseSqlite(serviceProvider.GetRequiredService<DbConnection>().ConnectionString);
//        }, ServiceLifetime.Singleton);
//        config.AddHostedService<Worker>();
//    })
//    .Build();

//await host.RunAsync();

