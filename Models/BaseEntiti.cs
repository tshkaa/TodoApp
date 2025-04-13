namespace TodoApp.Models;

public class BaseEntiti<TId> 
    where TId : IEquatable<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeletedAt { get; set; }

    public BaseEntiti()
    {
        CreatedAt = DateTime.UtcNow;
    }
}