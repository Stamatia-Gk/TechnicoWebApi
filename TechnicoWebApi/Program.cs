using Microsoft.EntityFrameworkCore;
using Technico.Data;
using Technico.Repositories.Implementations;
using Technico.Repositories.Interfaces;
using Technico.Services.Implementations;
using Technico.Services.Interfaces;
using Technico.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TechnicoDbContext>();
builder.Services.AddScoped<IRepairService , RepairService>();
builder.Services.AddScoped<IRepairRepository, RepairRepository>();
builder.Services.AddScoped<RepairValidator>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
