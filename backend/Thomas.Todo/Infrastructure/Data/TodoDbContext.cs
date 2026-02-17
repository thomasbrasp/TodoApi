using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema("todo");
    }
    
    public DbSet<Todo.Features.Todos.Entities.Todo> Todos => Set<Todo.Features.Todos.Entities.Todo>();
}
