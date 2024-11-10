// Team Project | European Dynamics | Code.Hub Project 2024
using System.ComponentModel.DataAnnotations;

namespace Technico.Models;

public class PropertyItem
{
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public string IdentificationNumber { get; set; } = string.Empty; // unique
    public string Address { get; set; } = string.Empty;
    public int ConstructionYear { get; set; }
    public PropertyType PropertyType { get; set; }
    public List<Owner> Owners { get; set; } = []; // a property has one or more owners
}
