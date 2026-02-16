using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var todo = await context.Set<Entities.Todo>().FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (todo != null) context.Todos.Remove(todo);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}