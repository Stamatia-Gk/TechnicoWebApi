

using CSharpFunctionalExtensions;
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IOwnerService
{
    Task<List<OwnerResponseDto>> GetAllOwners();
    Task<OwnerResponseDto> GetOwnerById(int id);
    Task<bool> CreateOwner(OwnerRequestDto ownerDto);
    //Task<OwnerResponseDto> UpdateOwner(int oldOwnerId, OwnerResponseDto newGetOwner);
    Task<bool> DeleteOwner(int ownerId);
    //Task<OwnerResponseDto> SearchOwner(string? vat, string? email);
    //Task<OwnerResponseDto> Login(string email, string password);
}
