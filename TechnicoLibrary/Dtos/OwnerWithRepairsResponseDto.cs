// Team Project | European Dynamics | Code.Hub Project 2024

namespace TechnicoWebApi.Dtos;

public class OwnerWithRepairsResponseDto: OwnerResponseDto
{
    public List<RepairDto>? OwnerRepairs { get; set; }
}
