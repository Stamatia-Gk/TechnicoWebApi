using Technico.Data;
using Technico.Services.Interfaces;
using Technico.Validator;
using TechnicoWebApi.Services.Implementations;
using FluentValidation.AspNetCore;
using FluentValidation;
using TechnicoWebApi.Repositories.Implementations;
using TechnicoWebApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<TechnicoDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<OwnerValidator>();

builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<PropertyValidator>();

builder.Services.AddScoped<IRepairService, RepairService>();
builder.Services.AddScoped<IRepairRepository, RepairRepository>();
builder.Services.AddScoped<RepairValidator>();

//builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
//builder.Services.AddValidatorsFromAssemblyContaining<OwnerDTOCreate>();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<TechnicoDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
