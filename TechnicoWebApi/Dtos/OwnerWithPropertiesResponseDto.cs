// Team Project | European Dynamics | Code.Hub Project 2024

namespace TechnicoWebApi.Dtos;

public class OwnerWithPropertiesResponseDto: OwnerResponseDto
{
    public List<PropertyDto>? OwnerProperties { get; set; }
}
