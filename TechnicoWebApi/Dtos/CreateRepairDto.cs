
namespace TechnicoWebApi.Dtos;

public class CreateRepairDto
{
    public List<OwnerResponseDto> ownerList = new();
    public RepairDto repairDto;
}
