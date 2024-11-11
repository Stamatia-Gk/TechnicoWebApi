// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Technico.DTO;
using Technico.Models;

namespace Technico.Services.Interfaces
{
    public interface IRepairService
    {
        Task<Result<RepairDTO>> CreateRepair(RepairDTO repairDto, int ownerId);
        Task<Result> DeleteRepair(int repairId);
        Task<Result<RepairDTO>> GetRepair(int id);
        Task<Result<List<RepairDTO>>> GetAllRepairs();
        Task<Result<List<RepairDTO>>> GetAllRepairsOfAnOwner(int id);
        Task<Result<List<RepairDTO>>> SearchRepair(DateTime startDate, DateTime endDate, int userId);
        Task<Result<RepairDTO>> UpdateRepair(int oldRepairId, RepairDTO newRepairDto);
    }
}