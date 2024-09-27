using Asp.Versioning.ApiExplorer;

using Markel.API.Extensions;
using Markel.Application;
using Markel.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
    
    app.SeedData();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

// used for testing
public partial class Program;


