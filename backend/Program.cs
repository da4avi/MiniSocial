using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniSocial.Controllers;
using MiniSocial.Data;
using MiniSocial.Models;
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

//adiciona o IPasswordHasher do ASP.NET CORE
builder.Services.AddScoped<IPasswordHasher<UserProfile>, PasswordHasher<UserProfile>>();

//services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PostService>();

//identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//cookie auth
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "auth";
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;

    options.Events.OnRedirectToLogin = ctx =>
    {
      ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
      return Task.CompletedTask;  
    };

    options.Events.OnRedirectToAccessDenied = ctx =>
    {
        ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

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
