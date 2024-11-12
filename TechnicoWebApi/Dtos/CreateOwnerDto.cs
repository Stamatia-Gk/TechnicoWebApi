// Team Project | European Dynamics | Code.Hub Project 2024

namespace TechnicoWebApi.Dtos;

public class CreateOwnerDto : GetOwnerDto
{
    public string Password { get; set; } = string.Empty;
}
