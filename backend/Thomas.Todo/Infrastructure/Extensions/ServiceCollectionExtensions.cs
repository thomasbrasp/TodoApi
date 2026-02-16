using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Todo.Validators;

namespace Todo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureModule(this IServiceCollection services)
    {
        services.AddMediatR(serviceConfiguration =>
        {
            serviceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            serviceConfiguration.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            serviceConfiguration.AddOpenBehavior(typeof(QueryValidationBehavior<,>));
        });

        return services;
    }
}