using System.Net;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Features.Todos.Commands;
using Todo.Features.Todos.Entities;
using Todo.Features.Todos.Models;
using Todo.Features.Todos.Queries;
using Todo.Infrastructure.Data;
using TodoApi.Interfaces;

namespace TodoApi.Routes;

public sealed class TodosRoutes : IEndpointRouteConfiguration
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("todos").WithTags("Todos");

        #region commands

        //create todo
        group.MapPost("", async ([FromServices] IMediator mediator, [FromBody] CreateTodo.Command command) =>
                {
                    var result = await mediator.Send(command);
                    return result;
                })
                .Produces<TodoItem>();

        //update todo
        group.MapPut("{id}", async (int id, [FromServices] IMediator mediator, [FromBody] UpdateTodo.Command command) =>
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                })
                .Produces((int)HttpStatusCode.NoContent);

        //delete todo
        group.MapDelete("{id}", async (int id, [FromServices] IMediator mediator, [FromBody] DeleteTodo.Command command) =>
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                })
                .Produces((int)HttpStatusCode.NoContent);


        //toggle todo complete
        group.MapPut("{id}/toggle-complete", async (int id, [FromServices] IMediator mediator, [FromBody] ToggleTodoComplete.Command command) =>
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                })
                .Produces((int)HttpStatusCode.NoContent);

        #endregion

        #region queries

        //gettodos
        group.MapGet("", async ([FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetTodos.Query());
                    return Results.Ok(result);
                })
                .Produces<List<TodoItem>>();

        //gettodo
        group.MapGet("{id}", async (int id, [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetTodo.Query(id));
                    return Results.Ok(result);
                })
                .Produces<TodoItem>();

        //getcompletedtodos
        group.MapGet("/complete", async ([FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetCompletedTodos.Query());
                    return Results.Ok(result);
                })
                .Produces<List<TodoItem>>();


        #endregion







        return builder;
    }
};