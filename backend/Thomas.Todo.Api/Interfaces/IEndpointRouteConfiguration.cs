namespace TodoApi.Interfaces;

public interface IEndpointRouteConfiguration
{
    IEndpointRouteBuilder Configure(IEndpointRouteBuilder builder);
}