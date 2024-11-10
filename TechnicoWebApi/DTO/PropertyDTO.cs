// Team Project | European Dynamics | Code.Hub Project 2024

using Technico.Models;

namespace Technico.DTO;

public class PropertyDTO()
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty; // unique
    public string Address { get; set; } = string.Empty;
    public int ConstructionYear { get; set; }
    public PropertyType PropertyType { get; set; }
}
