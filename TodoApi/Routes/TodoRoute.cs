using System.Net;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Routes;

public sealed class TodosRoutes : IEndpointRouteConfiguration
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("todos").WithTags("Todos");

        group.MapGet("", async (TodoDb db) =>
            {
                return await db.Set<Todo>()
                    .AsNoTracking()
                    .Select(x => new TodoItem(x))
                    .ToListAsync();
            })
            .Produces<List<TodoItem>>();


        group.MapGet("{id}", async (TodoDb db, int id) =>
            {
                return await db.Set<Todo>()
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(t => new TodoItem(t))
                    .FirstOrDefaultAsync();
            })
            .Produces<TodoItem>();

        group.MapGet("/complete", async (TodoDb db) =>
            {
                return await db.Set<Todo>()
                    .AsNoTracking()
                    .Where(t => t.IsComplete == true)
                    .Select(x => new TodoItem(x))
                    .ToListAsync();
            })
            .Produces<List<TodoItem>>();

        //create todo
        group.MapPost("", async (TodoItem todoItem, TodoDb db) =>
            {
                var todo = new Todo
                {
                    IsComplete = todoItem.IsComplete,
                    Name = todoItem.Name
                };

                db.Todos.Add(todo);
                await db.SaveChangesAsync();
            })
            .Produces<Todo>();

        //update todo
        group.MapPut("{id}", async (int id, TodoItem todoItem, TodoDb db) =>
            {
                var todo = await db.Todos.FindAsync(id);

                todo.Name = todoItem.Name;
                todo.IsComplete = todoItem.IsComplete;

                await db.SaveChangesAsync();
            })
            .Produces((int)HttpStatusCode.NoContent);

        group.MapPut("{id}/toggle-complete", async (int id, TodoItem todoItem, TodoDb db) =>
            {
                var todo = await db.Todos.FindAsync(id);
                
                todo.Name = todoItem.Name;
                todo.IsComplete = !todoItem.IsComplete;
                
                await db.SaveChangesAsync();
            })
            .Produces((int)HttpStatusCode.NoContent);

        //delete todo
        group.MapDelete("{id}", async (int id, TodoDb db) =>
            {
                if (await db.Todos.FindAsync(id) is Todo todo)
                {
                    db.Todos.Remove(todo);
                    await db.SaveChangesAsync();
                }
            })
            .Produces((int)HttpStatusCode.NoContent);


        return builder;
    }
};