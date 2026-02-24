using MiniSocial.Controllers;
using MiniSocial.Services;

var builder = WebApplication.CreateBuilder(args);

//habilita o uso da pasta controllers
builder.Services.AddControllers();

builder.Services.AddScoped<UserService>();

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // O Swagger é mais amigável que o "OpenApi" puro por enquanto

var app = builder.Build();

// 2. ISSO É ESSENCIAL: Ativa a página visual do Swagger para você testar
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
