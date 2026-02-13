using MediatR;
using Todo.Features.Todos.Models;
using Todo.Infrastructure.Data;

namespace Todo.Features.Todos.Commands;

public class UpdateTodo
{
    public sealed record Command(int Id, TodoItem TodoItem) : IRequest;

    internal sealed class Handler(TodoDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.Id], cancellationToken);

            todo.Name = request.TodoItem.Name;
            todo.IsComplete = request.TodoItem.IsComplete;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}