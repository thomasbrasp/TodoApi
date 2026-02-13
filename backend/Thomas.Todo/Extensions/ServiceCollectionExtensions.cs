using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureModule(this IServiceCollection services)
    {
        services.AddMediatR(serviceConfiguration => { serviceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });


        return services;
    }
}