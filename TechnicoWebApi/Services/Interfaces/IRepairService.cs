// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;
using Technico.Models;

namespace Technico.Services.Interfaces
{
    public interface IRepairService
    {
        Task<Result<RepairDTO>> CreateRepair(Repair repair, Owner owner);
        Task<Result> DeleteRepair(int repairId);
        Task<Result<RepairDTO>> GetRepair(int id);
        Task<Result<RepairDTO>> UpdateRepair(int oldRepairId, Repair newRepair);
    }
}