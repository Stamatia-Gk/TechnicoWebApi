// Team Project | European Dynamics | Code.Hub Project 2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}