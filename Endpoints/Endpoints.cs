using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Endpoints;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        // получить все todo
        app.MapGet("/todos", ([FromServices] ApplicationDataContext context) =>
        {
            return Results.Ok(context.TodoItems.ToList());
        });
        
        // получить todo по его ID 
        app.MapGet("/todos/{id}", (Guid id, 
            [FromServices] ApplicationDataContext context) =>
        {
            var todo = context.TodoItems.FirstOrDefault(x => x.Id == id);
            return todo is not null ? Results.Ok(todo) : Results.NotFound();
        });
        
        // создать todo
        app.MapPost("/todos", (TodoItem todo, [FromServices] ApplicationDataContext context) =>
        {
            context.TodoItems.Add(todo);
            context.SaveChanges();
            return Results.Created($"/todos/{todo.Id}", todo);
        });
        
        // изменить todo по его ID
        app.MapPut("/todos/{id}", (Guid id, TodoItem updatedTodo, 
            [FromServices] ApplicationDataContext context) =>
        {
            var todo = context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todo is not null)
            {
                context.TodoItems.Remove(todo);
                context.TodoItems.Add(updatedTodo);
                
                context.SaveChanges();
                // return Results.Ok(updatedTodo);
                // или
                return Results.NoContent();
            }
            return Results.NotFound();
        });
        
        // удалить todo по его ID
        app.MapDelete("/todos/{id}", (Guid id, [FromServices] ApplicationDataContext context) =>
        {
            var todo = context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todo is not null)
            {
                context.TodoItems.Remove(todo);
                context.SaveChanges();
                
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}