// Team Project | European Dynamics | Code.Hub Project 2024

namespace TechnicoWebApi.Dtos;

public class GetOwnerWithRepairsDto: GetOwnerDto
{
    public List<RepairDto>? OwnerRepairs { get; set; }
}
