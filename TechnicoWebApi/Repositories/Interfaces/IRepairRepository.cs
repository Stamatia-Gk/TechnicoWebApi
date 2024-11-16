// Team Project | European Dynamics | Code.Hub Project 2024

using Technico.Models;

namespace TechnicoWebApi.Repositories.Interfaces
{
    public interface IRepairRepository
    {
        Task<List<Repair>> GetRepairs();
        Task<List<Repair>> GetRepairsInProgress();
        Task<Repair?> GetRepairById(int id);
        Task<List<Repair?>> GetRepairsByOwnerId(int id);
        Task<bool> CreateRepair(Repair repair);
        Task<bool> UpdateRepair(Repair repair);
        Task<bool> DeleteRepair(Repair repair);
        Task<List<Repair>> Search(DateTime startDate, DateTime endDate, int ownerId=0);
        Task<bool> RepairExists(int id);
        Task<bool> Save();
    }
}