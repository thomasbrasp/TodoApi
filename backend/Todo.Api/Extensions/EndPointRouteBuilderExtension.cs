using System.Reflection;

namespace TodoApi.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void ConfigureEndpoints(this IEndpointRouteBuilder builder, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => typeof(IEndpointRouteConfiguration).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type) as IEndpointRouteConfiguration;
            instance?.Configure(builder);
        }
    }
}