﻿

using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IRepairService
{
    public Task<List<RepairDto>> GetRepairs();
    Task<List<RepairDto>> SearchRepairsByDateRange(DateTime startDateTime, DateTime endDateTime, int ownerId);
    public Task<RepairDto> GetRepairById(int id);
    public Task<RepairDto> CreateRepair(RepairDto repairDto, int ownerId);
    public Task<RepairDto> UpdateRepair(RepairDto repairDto, int id);
    public Task<List<RepairDto>> DeleteRepair(int id);

}
