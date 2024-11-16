
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IOwnerService
{
    Task<List<OwnerResponseDto>> GetAllOwners();
    Task<OwnerResponseDto> GetOwnerById(int id);
    Task<List<OwnerResponseDto>> SearchOwner(string? vat, string? email);
    Task<OwnerResponseDto> CreateOwner(OwnerRequestDto ownerDto);
    Task<OwnerResponseDto> UpdateOwner(int oldOwnerId, OwnerResponseDto newGetOwner);
    Task<List<OwnerResponseDto>> DeleteOwner(int ownerId);
    Task<OwnerResponseDto> Login(string email, string password);
}
