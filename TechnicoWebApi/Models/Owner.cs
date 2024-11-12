// Team Project | European Dynamics | Code.Hub Project 2024

using System.ComponentModel.DataAnnotations;
using Technico.Models;

namespace TechnicoWebApi.Models;

public class Owner
{
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public string VAT { get; set; } = string.Empty; // unique identifier
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Surname { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(250)]
    public string Email { get; set; } = string.Empty; // used as username
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    public OwnerType OwnerType { get; set; }

    public List<PropertyItem> Properties { get; set; } = []; // an owner can have one or more properties
    public List<Repair> AllRepairs { get; set; } = []; // am owner/property can have multiple repairs
}
