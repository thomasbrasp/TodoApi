using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Data;
using Todo.Validators;

namespace Todo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(serviceConfiguration =>
        {
            serviceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            serviceConfiguration.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            serviceConfiguration.AddOpenBehavior(typeof(QueryValidationBehavior<,>));
        });

        services.AddDbContext<TodoDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
                builder => { builder.MigrationsHistoryTable("__EFMigrationsHistory", "todos"); });
        });

        return services;
    }
}