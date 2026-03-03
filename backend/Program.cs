using Microsoft.EntityFrameworkCore;
using MiniSocial.Configurations;
using MiniSocial.Data;
using MiniSocial.Services;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<ProfileService>();

//identity
builder.Services.AddIdentityConfig();

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

//auth
app.UseAuthentication();
app.UseAuthorization();

//mapeia os controllers
app.MapControllers();

app.Run();
