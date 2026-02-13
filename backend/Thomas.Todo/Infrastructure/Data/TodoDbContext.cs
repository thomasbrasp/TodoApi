using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Data;

public class TodoDbContext: DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<Todo.Features.Todos.Entities.Todo> Todos => Set<Todo.Features.Todos.Entities.Todo>();
}