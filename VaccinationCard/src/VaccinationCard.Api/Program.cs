using VaccinationCard.Application;
using VaccinationCard.Infrastructure;
using VaccinationCard.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
//builder.Services.AddApplication();
//builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

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