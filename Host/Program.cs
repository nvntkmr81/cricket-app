using Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoreServices();
// Register rate limiting from Core extensions
builder.Services.AddAppRateLimiting();

var app = builder.Build();

// Global exception logging middleware
app.UseExceptionLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable the rate limiter middleware globally
app.UseAppRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
