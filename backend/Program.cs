using MiniSocial.Configurations;
using MiniSocial.Middlewares;
using MiniSocial.Services;

var builder = WebApplication.CreateBuilder(args);

//log config
builder.AddSerilogConfig();

//banco config
builder.Services.AddDbConfig(builder.Configuration);

//identity config
builder.Services.AddIdentityConfig();

//controllers
builder.Services.AddControllers();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<PostService>();

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
