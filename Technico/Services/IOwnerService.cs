

using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IOwnerService
{
    Task<List<OwnerResponseDto>> GetAllOwners();
    Task<OwnerResponseDto> GetOwnerById(int id);
    Task<OwnerResponseDto> CreateOwner(OwnerRequestDto ownerDto);
    Task<OwnerResponseDto> UpdateOwner(int oldOwnerId, OwnerResponseDto newOwner);
    Task<List<OwnerResponseDto>> DeleteOwner(int ownerId);
    //Task<OwnerResponseDto> SearchOwner(string? vat, string? email);
    //Task<OwnerResponseDto> Login(string email, string password);
}
