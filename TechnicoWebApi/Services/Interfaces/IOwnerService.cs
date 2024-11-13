// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using TechnicoWebApi.Dtos;

namespace Technico.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<Result<OwnerResponseDto>> GetOwnerById(int id);
        Task<Result<List<OwnerResponseDto>>> GetAllOwners();
        Task<Result<OwnerRequestDto>> CreateOwner(OwnerRequestDto createOwner);
        Task<Result<OwnerResponseDto>> UpdateOwner(int oldOwnerId, OwnerResponseDto newGetOwner);
        Task<Result> DeleteOwner(int ownerId);
        Task<Result<OwnerResponseDto>> SearchOwner(string? vat, string? email);
        Task<Result<OwnerResponseDto>> Login(string email, string password);
    }
}