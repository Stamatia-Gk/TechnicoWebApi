
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

namespace Technico.Models;

public class OwnersListAndPropertyDto : PropertyDto
{
    public List<Owner> Owners { get; set; } = new();
    public PropertyDto Property { get; set; }
}
