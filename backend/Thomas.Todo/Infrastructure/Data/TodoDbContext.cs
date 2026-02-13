using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    
}