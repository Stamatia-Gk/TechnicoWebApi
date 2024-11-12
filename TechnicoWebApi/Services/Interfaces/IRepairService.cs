// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;

namespace Technico.Services.Interfaces
{
    public interface IRepairService
    {
        Task<Result<List<RepairDTO>>> GetAllRepairs();
        Task<Result<RepairDTO>> GetRepairById(int id);
        Task<Result<List<RepairDTO>>> GetAllRepairsOfAnOwner(int id);
        Task<Result<RepairDTO>> CreateRepair(RepairDTO repairDto, int ownerId);
        Task<Result<RepairDTO>> UpdateRepair(int oldRepairId, RepairDTO newRepairDto);
        Task<Result> DeleteRepair(int repairId);
        Task<Result<List<RepairDTO>>> SearchRepair(DateTime startDate, DateTime endDate, int userId);
    }
}