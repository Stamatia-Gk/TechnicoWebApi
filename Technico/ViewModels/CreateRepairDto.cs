
using TechnicoWebApi.Dtos;

namespace Technico.ViewModels;

public class CreateRepairDto
{
    public List<OwnerResponseDto> ownerList = new();
    public RepairDto repairDto;
}
