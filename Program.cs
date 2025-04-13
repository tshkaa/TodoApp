namespace TodoApp;

public class Program
{
    public static void Main(string[] args)
    {
        // строитель приложения
        var builder = WebApplication.CreateBuilder();
        
        // строим приложение
        var app = builder.Build();
        
        // вымышленная база данных
        var todos = new List<Todo>();

        // endpoints:
        
        // получить все todo
        app.MapGet("/todos", () => Results.Ok(todos));
        // получить todo по его ID 
        app.MapGet("/todos/{id}", (int id) =>
        {
            var todo = todos.FirstOrDefault(x => x.Id == id);
            return todo is not null ? Results.Ok(todo) : Results.NotFound();
        });
        
        // создать todo
        app.MapPost("/todos", (Todo todo) =>
        {
            todos.Add(todo);
            return Results.Created($"/todos/{todo.Id}", todo);
        });
        
        // изменить todo по его ID
        app.MapPut("/todos/{id}", (int id, Todo updatedTodo) =>
        {
            var todo = todos.FirstOrDefault(x => x.Id == id);
            if (todo is not null)
            {
                todos.Remove(todo);
                todos.Add(updatedTodo);
                // return Results.Ok(updatedTodo);
                // или
                return Results.NoContent();
            }
            return Results.NotFound();
        });
        
        // удалить todo по его ID
        app.MapDelete("/todos/{id}", (int id) =>
        {
            var todo = todos.FirstOrDefault(x => x.Id == id);
            if (todo is not null)
            {
                todos.Remove(todo);
                return Results.NoContent();
            }
            return Results.NotFound();
        });
        
        app.Run();
    }
}