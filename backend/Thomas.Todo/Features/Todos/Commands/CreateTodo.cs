using MediatR;
using Todo.Features.Todos.Models;
using Todo.Infrastructure.Data;

namespace Todo.Features.Todos.Commands;

public static class CreateTodo
{
    public sealed record Command(string Name, bool IsComplete) : IRequest<TodoItem>;

    internal sealed class Handler(TodoDbContext context) : IRequestHandler<Command, TodoItem>
    {
        public async Task<TodoItem> Handle(Command request, CancellationToken cancellationToken)
        {
            var todo = new Entities.Todo
            {
                    IsComplete = request.IsComplete,
                    Name = request.Name
            };

            context.Add(todo);
            await context.SaveChangesAsync(cancellationToken);

            return new TodoItem(todo);
        }
    }
}