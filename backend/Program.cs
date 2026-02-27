using Microsoft.AspNetCore.Identity;
using MiniSocial.Controllers;
using MiniSocial.Models;
using MiniSocial.Services;

var builder = WebApplication.CreateBuilder(args);

//controllers
builder.Services.AddControllers();

//adiciona o IPasswordHasher do ASP.NET CORE
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

//services
builder.Services.AddScoped<UserService>();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//http = https
app.UseHttpsRedirection();

//mapeia os controllers
app.MapControllers();

app.Run();
