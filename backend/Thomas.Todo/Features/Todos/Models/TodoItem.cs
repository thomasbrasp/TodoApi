namespace Todo.Features.Todos.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public TodoItem(Entities.Todo todoItem) =>
            (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
}