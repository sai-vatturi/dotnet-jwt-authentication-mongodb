using AuthApp.Config;
using AuthApp.Repositories;
using AuthApp.Services;
using AuthApp.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddSingleton<UserServiceDB>();
builder.Services.AddSingleton<JWTService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
