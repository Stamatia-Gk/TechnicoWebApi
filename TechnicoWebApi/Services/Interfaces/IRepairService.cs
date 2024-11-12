// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using TechnicoWebApi.Dtos;

namespace Technico.Services.Interfaces
{
    public interface IRepairService
    {
        Task<Result<List<RepairDto>>> GetAllRepairs();
        Task<Result<List<RepairDto>>> GetOngoingRepairs();
        Task<Result<RepairDto>> GetRepairById(int id);
        Task<Result<List<RepairDto>>> GetAllRepairsOfAnOwner(int id);
        Task<Result<RepairDto>> CreateRepair(RepairDto repairDto, int ownerId);
        Task<Result<RepairDto>> UpdateRepair(int oldRepairId, RepairDto newRepairDto);
        Task<Result> DeleteRepair(int repairId);
        Task<Result<List<RepairDto>>> SearchRepair(DateTime startDate, DateTime endDate, int userId);
    }
}