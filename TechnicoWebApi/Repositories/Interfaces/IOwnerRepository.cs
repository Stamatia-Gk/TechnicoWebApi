// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;
using Technico.Data;

namespace Technico.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetOwners();
        Task<Owner?> GetOwnerById(int id);
        //Task<List<PropertyItem>> GetPropertyItemByOwnerId(int id);
        //Task<List<Repair>> GetRepairByOwnerId(int id);
        Task<bool> CreateOwner(Owner owner);
        Task<bool> UpdateOwner(Owner owner);
        Task<bool> DeleteOwner(Owner owner);
        Task<bool> OwnerExists(string vatNumber);
        Task<bool> Save();
    }
}