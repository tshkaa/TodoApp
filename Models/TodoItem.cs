namespace TodoApp.Models;

public class TodoItem : BaseEntiti<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
}