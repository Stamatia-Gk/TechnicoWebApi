// Team Project | European Dynamics | Code.Hub Project 2024

using TechnicoWebApi.Models;

namespace TechnicoWebApi.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetOwners();
        Task<Owner?> GetOwnerById(int id);
        Task<Owner?> GetOwnerByVat(string vat);
        Task<bool> CreateOwner(Owner owner);
        Task<bool> UpdateOwner(Owner owner);
        Task<bool> DeleteOwner(Owner owner);
        Task<Owner?> Search(string vat, string email);
        Task<bool> OwnerExists(int id, string vatNumber, string email, string phone);
        Task<bool> Save();
        Task<Owner> Login(string email, string password);
    }
}