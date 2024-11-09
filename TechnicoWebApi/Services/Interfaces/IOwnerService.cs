// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;
using CSharpFunctionalExtensions;
using Technico.DTO;

namespace Technico.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<Result<OwnerDTOCreate>> CreateOwner(OwnerDTOCreate owner);
        Task<Result> DeleteOwner(int ownerId);
        Task<Result<OwnerDTO>> GetOwner(int id);
        Task<Result<List<OwnerDTO>>> GetAllOwners();
        Task<Result<OwnerDTO>> UpdateOwner(int oldOwnerId, OwnerDTO newOwner);
    }
}