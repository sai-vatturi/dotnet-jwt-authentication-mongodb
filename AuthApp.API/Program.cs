using AuthApp.Services;
using AuthApp.Services.Config;
using AuthApp.Services.Repositories;
using AuthApp.Services.Validators;
using AuthApp.Utility.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Enable attribute-based controllers.

// Configure Swagger/OpenAPI for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserServiceDB>();
builder.Services.AddScoped<UserValidator>();
builder.Services.AddScoped<JWTService>();

// Bind strongly-typed configuration for the database (assuming DatabaseSettings is in AuthApp.Utility).
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // Map attribute-based routes.

app.Run();
