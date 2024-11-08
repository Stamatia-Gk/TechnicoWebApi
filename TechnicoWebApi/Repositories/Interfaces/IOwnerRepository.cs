// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;
using Technico.Data;

namespace Technico.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        Task<bool> CreateOwner(Owner owner);
        Task<bool> DeleteOwner(Owner owner);
        Task<Owner?> GetOwner(int id);
        Task<List<Owner>> GetOwners();
        Task<bool> OwnerExists(string vatNumber);
        Task<bool> Save();
        Task<bool> UpdateOwner(Owner owner);
    }
}