namespace TodoApi.Routes;

public sealed class RechtspersonenRoutes : IEndpointRouteConfiguration
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("todos").WithTags("Todos");

        group.MapGet();
    }
};