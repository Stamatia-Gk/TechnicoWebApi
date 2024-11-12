// Team Project | European Dynamics | Code.Hub Project 2024

using Technico.Models;
using TechnicoWebApi.Models;

namespace TechnicoWebApi.Dtos;

public class PropertyDto()
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty; // unique
    public string Address { get; set; } = string.Empty;
    public int ConstructionYear { get; set; }
    public PropertyType PropertyType { get; set; }
}
