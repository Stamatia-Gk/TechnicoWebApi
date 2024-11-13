// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.Services.Interfaces;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;
using TechnicoWebApi.Repositories.Interfaces;

namespace TechnicoWebApi.Services.Implementations;
public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<Result<List<OwnerResponseDto>>> GetAllOwners()
    {
        var ownersList = await _ownerRepository.GetOwners();
        var ownerDtos = ownersList.Select(owner => Converters.ConvertToOwnerDto(owner));
        return ownerDtos.ToList();
    }

    public async Task<Result<OwnerResponseDto>> GetOwnerById(int id)
    {
        var owner = await _ownerRepository.GetOwnerById(id);
        if (owner == null)
        {
            return Result.Failure<OwnerResponseDto>("Owner not found!");
        }

        return Result.Success(Converters.ConvertToOwnerDto(owner));
    }

    public async Task<Result<OwnerRequestDto>> CreateOwner(OwnerRequestDto createOwnerDto)
    {
        Owner ow1 = Converters.ConvertToOwnerPw(createOwnerDto);

        var ownerCreated = await _ownerRepository.CreateOwner(ow1);
        if (!ownerCreated)
        {
            return Result.Failure<OwnerRequestDto>("Owner could not be saved (already exists)!");
        }

        return Result.Success(createOwnerDto);
    }

    public async Task<Result<OwnerResponseDto>> UpdateOwner(int oldOwnerId, OwnerResponseDto newGetOwnerDto)
    {
        var ownerToUpdate = await _ownerRepository.GetOwnerById(oldOwnerId);
        if (ownerToUpdate == null)
        {
            return Result.Failure<OwnerResponseDto>("The owner you want to update was not found!");
        }

        var newOwner = Converters.ConvertToOwner(newGetOwnerDto);

        var ownerFieldsAlreadyUsed = await _ownerRepository.OwnerExists(newOwner.Id, newOwner.VAT, newOwner.Email, newOwner.PhoneNumber);

        if(ownerFieldsAlreadyUsed)
        {
            return Result.Failure<OwnerResponseDto>("Update failed (duplicated info with existing owner).");
        }

        ownerToUpdate = Clone(ownerToUpdate, newOwner);
        var ownerUpdated = await _ownerRepository.UpdateOwner(ownerToUpdate);

        if (!ownerUpdated)
        {
            return Result.Failure<OwnerResponseDto>("Update failed!");
        }

        return Result.Success(newGetOwnerDto);
    }

    public async Task<Result> DeleteOwner(int ownerId)
    {
        var ownerToDelete = await _ownerRepository.GetOwnerById(ownerId);

        if (ownerToDelete == null)
        {
            return Result.Failure("This owner does not exist");
        }

        var ownerDeleted = await _ownerRepository.DeleteOwner(ownerToDelete);
        return ownerDeleted ? Result.Success("Owner successfully deleted.") : Result.Failure("Delete failed.");
    }

    public async Task<Result<OwnerResponseDto>> SearchOwner(string? vat, string? email)
    {
        var owner = await _ownerRepository.Search(vat, email);
        if(owner == null)
        {
            return Result.Failure<OwnerResponseDto>("No owner found with the specified criteria.");
        }

        var ownersDTO = Converters.ConvertToOwnerDto(owner);
        return Result.Success(ownersDTO);
    }

    private static Owner Clone(Owner oldOwner, Owner newOwner)
    {
        if (newOwner.VAT != null) oldOwner.VAT = newOwner.VAT;
        if (newOwner.Name != null) oldOwner.Name = newOwner.Name;
        if (newOwner.Surname != null) oldOwner.Surname = newOwner.Surname;
        if (newOwner.Address != null) oldOwner.Address = newOwner.Address;
        if (newOwner.PhoneNumber != null) oldOwner.PhoneNumber = newOwner.PhoneNumber;
        if (newOwner.Email != null) oldOwner.Email = newOwner.Email;
        if (newOwner.OwnerType != null) oldOwner.OwnerType = newOwner.OwnerType;
        if (newOwner.Properties != null) oldOwner.Properties = newOwner.Properties;
        if (newOwner.AllRepairs != null) oldOwner.AllRepairs = newOwner.AllRepairs;

        return oldOwner;
    }

    public async Task<Result<OwnerResponseDto>> Login(string email, string password)
    {
        var owner = await _ownerRepository.Login(email, password);
        if(owner == null)
        {
            return Result.Failure<OwnerResponseDto>("Invalid credentials!");
        }

        var ownerDTO = Converters.ConvertToOwnerDto(owner);
        return Result.Success(ownerDTO);
    }
}
