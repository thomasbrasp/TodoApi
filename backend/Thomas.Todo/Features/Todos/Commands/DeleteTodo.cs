using MediatR;
using Todo.Features.Todos.Models;
using Todo.Infrastructure.Data;

namespace Todo.Features.Todos.Commands;

public class DeleteTodo
{
    public sealed record Command(int Id) : IRequest;


    internal sealed class Handler(TodoDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            if (await context.Todos.FindAsync([request.Id], cancellationToken) is Entities.Todo todo)
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}