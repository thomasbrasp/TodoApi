using System.Net;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Features.Todos.Commands;
using Todo.Features.Todos.Entities;
using Todo.Features.Todos.Models;
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
        group.MapPut("{id}", async ([FromServices] IMediator mediator, [FromBody] UpdateTodo.Command command) =>
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                })
                .Produces((int)HttpStatusCode.NoContent);

        //delete todo
        group.MapDelete("{id}", async ([FromServices] IMediator mediator, [FromBody] DeleteTodo.Command command) =>
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                })
                .Produces((int)HttpStatusCode.NoContent);


        //toggle todo complete
        group.MapPut("{id}/toggle-complete", async ([FromServices] IMediator mediator, [FromBody] ToggleTodoComplete.Command command) =>
                {
                    await mediator.Send(command);
                    return Results.NoContent();
                })
                .Produces((int)HttpStatusCode.NoContent);

        #endregion


        group.MapGet("", async (TodoDbContext db) =>
                {
                    return await db.Set<Todo.Features.Todos.Entities.Todo>()
                            .AsNoTracking()
                            .Select(x => new TodoItem(x))
                            .ToListAsync();
                })
                .Produces<List<TodoItem>>();


        group.MapGet("{id}", async (TodoDbContext db, int id) =>
                {
                    return await db.Set<Todo.Features.Todos.Entities.Todo>()
                            .AsNoTracking()
                            .Where(x => x.Id == id)
                            .Select(t => new TodoItem(t))
                            .FirstOrDefaultAsync();
                })
                .Produces<TodoItem>();

        group.MapGet("/complete", async (TodoDbContext db) =>
                {
                    return await db.Set<Todo.Features.Todos.Entities.Todo>()
                            .AsNoTracking()
                            .Where(t => t.IsComplete == true)
                            .Select(x => new TodoItem(x))
                            .ToListAsync();
                })
                .Produces<List<TodoItem>>();

        return builder;
    }
};