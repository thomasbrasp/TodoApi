using MediatR;
using Todo.Features.Todos.Models;
using Todo.Infrastructure.Data;

namespace Todo.Features.Todos.Commands;

public class ToggleTodoComplete
{
    public sealed record Command(int Id, TodoItem TodoItem) : IRequest;

    internal sealed class Handler(TodoDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            if (await context.Todos.FindAsync([request.Id], cancellationToken) is Entities.Todo todo)
            {

                todo.IsComplete = request.TodoItem.IsComplete;

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }

}