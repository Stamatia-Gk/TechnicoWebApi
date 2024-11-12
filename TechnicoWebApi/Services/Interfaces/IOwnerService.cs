// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using TechnicoWebApi.Dtos;

namespace Technico.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<Result<GetOwnerDto>> GetOwnerById(int id);
        Task<Result<List<GetOwnerDto>>> GetAllOwners();
        Task<Result<CreateOwnerDto>> CreateOwner(CreateOwnerDto createOwner);
        Task<Result<GetOwnerDto>> UpdateOwner(int oldOwnerId, GetOwnerDto newGetOwner);
        Task<Result> DeleteOwner(int ownerId);
        Task<Result<GetOwnerDto>> SearchOwner(string? vat, string? email);
    }
}