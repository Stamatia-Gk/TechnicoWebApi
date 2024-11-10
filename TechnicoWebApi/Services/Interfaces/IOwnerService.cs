// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;

namespace Technico.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<Result<OwnerDTO>> GetOwner(int id);
        Task<Result<List<OwnerDTO>>> GetAllOwners();
        Task<Result<OwnerDTOCreate>> CreateOwner(OwnerDTOCreate owner);
        Task<Result<OwnerDTO>> UpdateOwner(int oldOwnerId, OwnerDTO newOwner);
        Task<Result> DeleteOwner(int ownerId);
    }
}