using Microsoft.EntityFrameworkCore;
using MiniSocial.Configurations;
using MiniSocial.Data;
using MiniSocial.Middlewares;
using MiniSocial.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger(); 

var builder = WebApplication.CreateBuilder(args);

//log
builder.Host.UseSerilog();

//banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

//controllers
builder.Services.AddControllers();

//services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<PostService>();

//identity
builder.Services.AddIdentityConfig();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//pipeline

//middleware pra pegar exception
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//http = https
app.UseHttpsRedirection();

//auth
app.UseAuthentication();
app.UseAuthorization();

//mapeia os controllers
app.MapControllers();

app.Run();
