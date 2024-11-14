// Team Project | European Dynamics | Code.Hub Project 2024
using TechnicoLibrary.Models;
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
                {
                    Id = 1,
                    VAT = "12345678910",
                    Name = "Alice",
                    Surname = "Smith",
                    Address = "123 Elm St",
                    PhoneNumber = "6971234567",
                    Email = "alice.smith@example.com",
                    Password = "A1!abcde",
                    OwnerType = OwnerType.Admin
                },

                new Owner
                {   
                    Id = 2,
                    VAT = "12345678901",            
                    Name = "Johnathan",              
                    Surname = "Doe",                     
                    Address = "123 Main St, Anytown, USA",
                    PhoneNumber = "6987523641",         
                    Email = "john.doe@example.com",       
                    OwnerType = OwnerType.Owner,               
                    Password = "Secure@123" 
                }
             );
    }
}