﻿// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;

namespace Technico.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetOwners();
        Task<Owner?> GetOwnerById(int id);
        Task<Owner?> GetOwnerByVAT(string vat);
        Task<bool> CreateOwner(Owner owner);
        Task<bool> UpdateOwner(Owner owner);
        Task<bool> DeleteOwner(Owner owner);
        Task<List<Owner>> Search(string vat, string email);
        Task<bool> OwnerExists(int id, string vatNumber, string email, string phone);
        Task<bool> Save();
    }
}