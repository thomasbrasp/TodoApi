using MediatR;
using Todo.Features.Todos.Models;
using Todo.Infrastructure.Data;

namespace Todo.Features.Todos.Commands;

public class UpdateTodo
{
    public sealed record Command(int Id, string Name, bool IsComplete) : IRequest;

    internal sealed class Handler(TodoDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = await context.Todos.FindAsync([request.Id], cancellationToken);

            if (todo != null)
            {
                todo.Name = request.Name;
                todo.IsComplete = request.IsComplete;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}