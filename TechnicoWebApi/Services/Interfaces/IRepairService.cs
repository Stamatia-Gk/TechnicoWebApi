// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Technico.DTO;
using Technico.Models;

namespace Technico.Services.Interfaces
{
    public interface IRepairService
    {
        Task<Result<RepairDTOEmployee>> CreateRepair(RepairDTOEmployee repairDto, int ownerId);
        Task<Result> DeleteRepair(int repairId);
        Task<Result<RepairDTOEmployee>> GetRepair(int id);
        Task<Result<List<RepairDTOEmployee>>> GetAllRepairs();
        Task<Result<List<RepairDTOEmployee>>> SearchRepair(DateTime startDate, DateTime endDate, int userId);
        Task<Result<RepairDTOEmployee>> UpdateRepair(int oldRepairId, RepairDTOEmployee newRepairDto);
    }
}