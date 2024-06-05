using Microsoft.AspNetCore.Http.HttpResults;
using ToDoApp.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var todos = new List<Todo>();

app.MapGet("/todos", () => todos);

app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound> (int id) => 
{
    var todo = todos.FirstOrDefault(x => x.Id == id);
    return todo == null ? TypedResults.NotFound() : TypedResults.Ok(todo);
});

app.MapPost("/todos", (Todo task) => 
{
    todos.Add(task);
    return TypedResults.Created("/todos/{id}", task);
});

app.MapDelete("/todo/{id}", (int id) =>
{
    todos.RemoveAll(x => x.Id == id);
    return TypedResults.NoContent();
});

app.Run();