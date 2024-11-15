

using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IRepairService
{
    public Task<List<RepairDto>> GetRepairs();
    public Task<List<RepairDto>> GetOngoingRepairs();
    public Task<List<RepairDto>> SearchRepairsByDateRange(DateTime startDateTime, DateTime endDateTime, int ownerId);
    public Task<RepairDto> GetRepairById(int id);
    public Task<RepairDto> CreateRepair(RepairDto repairDto, int ownerId);
    public Task<RepairDto> UpdateRepair(RepairDto repairDto, int id);
    public Task<List<RepairDto>> DeleteRepair(int id);
    public Task<List<RepairDto>> OwnerRepairs(int id);
}
