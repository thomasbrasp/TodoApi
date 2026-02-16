using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Features.Todos.Models;
using Todo.Infrastructure.Data;

namespace Todo.Features.Todos.Queries;

public class GetTodos
{
    public sealed record Query : IRequest<List<TodoItem>>;


    internal sealed class Handler(TodoDbContext context) : IRequestHandler<Query, List<TodoItem>>
    {
        public async Task<List<TodoItem>> Handle(Query request, CancellationToken cancellationToken)
        {
            var todos = context.Set<Todo.Features.Todos.Entities.Todo>();
            return await todos
                    .AsNoTracking()
                    .Select(x => new TodoItem(x))
                    .ToListAsync(cancellationToken);
        }
    }
}
