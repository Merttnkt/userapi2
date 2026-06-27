using Microsoft.EntityFrameworkCore;
using UserapiEfCore.Data;
using UserapiEfCore.Models;

var builder = WebApplication.CreateBuilder(args);   


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=users.db"));

var app = builder.Build();

app.MapGet("/users", (AppDbContext context) =>
{
    var users = context.Users.ToList();
    return users;
});

app.MapPost("/users", (AppDbContext context , User user)  =>
{
    context.Users.Add(user);
    context.SaveChanges();
    return user;
});

app.MapDelete("/users/{id}", (AppDbContext context, int id) =>
{
    var user = context.Users.FirstOrDefault(x => x.Id == id);

    if (user == null)
    {
        return Results.NotFound("User not found");
    }

    context.Users.Remove(user);
    context.SaveChanges();

    return Results.Ok("User successfully deleted");
});

app.MapGet("/users/{id}", (AppDbContext context, int id) =>
{
    var user = context.Users.FirstOrDefault(x => x.Id == id);
    if (user == null){
        return Results.NotFound("User not found");
    }
    return Results.Ok(user);
});

app.MapPut("/users/{id}", (AppDbContext context, int id , User updateUser) =>
{
    var user = context.Users.FirstOrDefault(x => x.Id == id);
    if (user == null)
    {
        return Results.NotFound("User not found");
    }
    user.Name = updateUser.Name;
    user.Lastname = updateUser.Lastname;
    user.Age = updateUser.Age;
    context.SaveChanges();
    return Results.Ok("User successfully updated");

});

app.Run(); 
