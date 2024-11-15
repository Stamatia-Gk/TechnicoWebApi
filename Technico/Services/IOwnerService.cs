﻿
using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IOwnerService
{
    Task<List<OwnerResponseDto>> GetAllOwners();
    Task<OwnerResponseDto> GetOwnerById(int id);
    Task<OwnerResponseDto> SearchOwner(string vat = null, string email = null);
    Task<OwnerResponseDto> CreateOwner(OwnerRequestDto ownerDto);
    Task<OwnerResponseDto> UpdateOwner(int oldOwnerId, OwnerResponseDto newGetOwner);
    Task<List<OwnerResponseDto>> DeleteOwner(int ownerId);
    Task<OwnerResponseDto> Login(string email, string password);
}
