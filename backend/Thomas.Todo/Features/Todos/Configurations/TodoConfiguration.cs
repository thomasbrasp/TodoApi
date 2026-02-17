using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Todo.Features.Todos.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Entities.Todo>
{
    public void Configure(EntityTypeBuilder<Entities.Todo> builder)
    {
        builder.ToTable("Todos", "todos");
        
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Name).HasMaxLength(10);
        builder.Property(t => t.Secret).HasMaxLength(20);
    }
}

