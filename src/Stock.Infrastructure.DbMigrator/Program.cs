using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stock.Infrastructure.Contexts;

namespace Stock.Infrastructure.DbMigrator;

public class Program
{
    public static async Task Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        var host = CreateHostBuilder(args, environment).Build();
        await ApplyMigrationsAsync(host);
    }

    private static IHostBuilder CreateHostBuilder(string[] args, string environment)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.Sources.Clear();
                config
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) => { ConfigureDatabase(services, context.Configuration); });
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        Console.WriteLine(connectionString);
        services.AddDbContext<LiveStockDbContext>(options =>
        {
            options.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(typeof(LiveStockDbContext).Assembly.FullName));
        });
    }

    private static async Task ApplyMigrationsAsync(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LiveStockDbContext>();

        Console.WriteLine("Applying migrations...");
        await dbContext.Database.MigrateAsync();
        Console.WriteLine("Migration complete.");
    }
}