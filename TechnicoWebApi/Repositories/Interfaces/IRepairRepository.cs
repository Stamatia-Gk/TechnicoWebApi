// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;

namespace Technico.Repositories.Interfaces
{
    public interface IRepairRepository
    {
        Task<List<Repair>> GetRepairs();
        Task<Repair?> GetRepairById(int id);
        Task<bool> CreateRepair(Repair repair);
        Task<bool> UpdateRepair(Repair repair);
        Task<bool> DeleteRepair(Repair repair);
        Task<List<Repair>> Search(DateTime startDate, DateTime endDate, int ownerId);
        Task<bool> RepairExists(int id);
        Task<bool> Save();
    }
}