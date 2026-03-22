using DevFreela.Handlers;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddExceptionHandler<APIExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.Configure<FreelanceTotalCostModel>(
    builder.Configuration.GetSection("FreelanceTotalCostConfig"));

// builder.Services.AddDbContext<DevFreelaDbContext>(o => o.UseInMemoryDatabase("devfreeladb"));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
builder.Services.AddDbContext<DevFreelaDbContext>(o => o.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();

app.MapControllers();

app.Run();