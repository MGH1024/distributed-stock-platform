using MGH.Core.Infrastructure.Persistence.Base;
using MGH.Core.Infrastructure.Persistence.EF.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stock.Domain;
using Stock.Domain.LiveStocks;
using Stock.Domain.LiveStocks.Factories;
using Stock.Domain.LiveStocks.Policies;
using Stock.Domain.Outboxes;
using Stock.Infrastructure.Contexts;
using Stock.Infrastructure.Repositories;

namespace Stock.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static void AddSettings(this HostApplicationBuilder builder)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddConfiguration(builder.Configuration)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }

    public static void AddFactories(this IServiceCollection services)
    {
        services.AddScoped<ILiveStockFactory,LiveStockFactory>();
        services.AddScoped<ILiveStockPolicy,StorePolicy>();
    }

    public static void RegisterInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<AuditFieldsInterceptor>();
        services.AddSingleton<RemoveCacheInterceptor>();
        services.AddSingleton<AuditEntityInterceptor>();
        services.AddSingleton<OutboxEntityInterceptor>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUow, UnitOfWork>();
        services.AddScoped<ILiveStockRepository, LiveStockRepository>();
        services.AddScoped<IOutboxStore, OutboxMessageRepository>();
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
    }

    public static void AddDbContextSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<LiveStockDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString, a => { a.EnableRetryOnFailure(); })
                .AddInterceptors(
                    sp.GetRequiredService<AuditFieldsInterceptor>(),
                    sp.GetRequiredService<RemoveCacheInterceptor>(),
                    sp.GetRequiredService<AuditEntityInterceptor>(),
                    sp.GetRequiredService<OutboxEntityInterceptor>())
                .LogTo(Console.Write, LogLevel.Information);
        });
    }
}