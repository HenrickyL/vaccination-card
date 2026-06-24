using DotNetEnv;
using VaccinationCard.Api.Extensions;
using VaccinationCard.Application;
using VaccinationCard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../../.env");

builder.Services.AddOpenApi();

// Services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
//builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Middleware

app.UseApiExceptions();
app.UseHttpsRedirection();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();