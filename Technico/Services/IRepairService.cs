

using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IRepairService
{
    public Task<List<RepairDto>> GetRepairs();
    public Task<RepairDto> GetRepairById(int id);
    public Task<RepairDto> CreateRepair(RepairDto repairDto, int ownerId);
    public Task<List<RepairDto>> DeleteRepair(int id);

}
