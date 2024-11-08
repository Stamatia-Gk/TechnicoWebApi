using Technico.Data;
using Technico.Repositories.Implementations;
using Technico.Repositories.Interfaces;
using Technico.Services.Interfaces;
using TechnicoWebApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddDbContext<TechnicoDbContext>();


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
