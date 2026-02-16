using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Features.Todos.Models;
using Todo.Infrastructure.Data;

namespace Todo.Features.Todos.Queries;

public class GetTodo
{
    public sealed record Query(int Id) : IRequest<TodoItem?>;

    internal sealed class Handler(TodoDbContext context) : IRequestHandler<Query, TodoItem?>
    {
        public async Task<TodoItem?> Handle(Query request, CancellationToken cancellationToken)
        {
            var todos = context.Set<Todo.Features.Todos.Entities.Todo>();

            return await todos
                    .AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Select(t => new TodoItem(t))
                    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}