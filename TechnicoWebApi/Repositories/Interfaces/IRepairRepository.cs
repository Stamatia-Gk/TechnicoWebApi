// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;

namespace Technico.Repositories.Interfaces
{
    public interface IRepairRepository
    {
        Task<bool> CreateRepair(Repair repair, Owner owner);
        Task<bool> DeleteRepair(Repair repair);
        Task<Repair?> GetRepair(int id);
        Task<List<Repair>> GetRepairs();
        Task<bool> RepairExists(int id);
        Task<bool> Save();
        Task<bool> UpdateRepair(Repair repair);
    }
}