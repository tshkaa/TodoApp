using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Endpoints;
using TodoApp.Models;

namespace TodoApp;

public class Program
{
    public static void Main(string[] args)
    {
        // строитель приложения
        var builder = WebApplication.CreateBuilder();
        
        builder.Services.AddDbContext<ApplicationDataContext>(options => 
            options.UseInMemoryDatabase("InMemory"));
        
        // строим приложение
        var app = builder.Build();
        
        // вымышленная база данных
        var todos = new List<TodoItem>();
        
        // endpoints:
        app.MapEndpoints();
        
        app.Run();
    }
}