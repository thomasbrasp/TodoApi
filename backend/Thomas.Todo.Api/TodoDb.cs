using Microsoft.EntityFrameworkCore;


namespace TodoApi;

class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }

    public DbSet<Todo.Features.Todos.Entities.Todo> Todos => Set<Todo.Features.Todos.Entities.Todo>();
}