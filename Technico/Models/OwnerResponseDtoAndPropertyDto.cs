
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

namespace Technico.Models;

public class OwnerResponseDtoAndPropertyDto : PropertyDto
{
    public OwnerResponseDto Owner { get; set; }
    public PropertyDto Property { get; set; }
}
