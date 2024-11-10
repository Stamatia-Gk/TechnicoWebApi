// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;
using Microsoft.EntityFrameworkCore;

namespace Technico.Data;

public class TechnicoDbContext : DbContext
{
    public DbSet<Owner>? Owners { get; set; }
    public DbSet<PropertyItem>? Properties { get; set; }
    public DbSet<Repair>? Repairs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source = (local); Initial Catalog = technicowebapi; Integrated Security = True; TrustServerCertificate = True;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>()
            .HasIndex(o => o.VAT)
            .IsUnique();

        modelBuilder.Entity<Owner>()
            .HasIndex(o => o.Email)
            .IsUnique();

        modelBuilder.Entity<Owner>()
            .HasIndex(o => o.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<PropertyItem>()
            .HasIndex(p => p.IdentificationNumber)
            .IsUnique();

        modelBuilder.Entity<Owner>()
            .HasData(
                new Owner
                {   Id = 1,
                    VAT = "12345678910",
                    Name = "Alice",
                    Surname = "Smith",
                    Address = "123 Elm St",
                    PhoneNumber = "123-456-7890",
                    Email = "alice.smith@example.com",
                    Password = "A1!abcde",
                    OwnerType = OwnerType.Employee // Replace with a valid type
                },
                
                new Owner
                {   Id = 2,
                    VAT = "12345678901",            
                    Name = "Johnathan",              
                    Surname = "Doe",                     
                    Address = "123 Main St, Anytown, USA",
                    PhoneNumber = "123-456-7892",         
                    Email = "john.doe@example.com",
                    Password = "Secure@123",
                    OwnerType = OwnerType.Owner
                }
            );
        /*
         public int Id { get; set; }
    public string VAT { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public OwnerType OwnerType { get; set; }
    public List<PropertyItem> Properties { get; set; } = []; // an owner can have one or more properties
    public List<Repair> AllRepairs { get; set; } = [];*/
    }
}