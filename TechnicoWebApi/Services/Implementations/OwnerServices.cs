﻿// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;
using Technico.Models;
using Technico.Repositories.Implementations;
using Technico.Repositories.Interfaces;
using Technico.Services.Interfaces;

namespace TechnicoWebApi.Services.Implementations;
public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<Result<List<OwnerDTO>>> GetAllOwners()
    {
        var ownersList = await _ownerRepository.GetOwners();
        var ownerDtos = ownersList.Select(owner => Converters.ConvertToOwnerDTO(owner));
        return ownerDtos.ToList();
    }

    public async Task<Result<OwnerDTO>> GetOwnerById(int id)
    {
        var owner = await _ownerRepository.GetOwnerById(id);
        if (owner == null)
        {
            return Result.Failure<OwnerDTO>("Owner not found!");
        }

        return Result.Success(Converters.ConvertToOwnerDTO(owner));
    }

    public async Task<Result<OwnerDTOCreate>> CreateOwner(OwnerDTOCreate ownerDto)
    {
        Owner ow1 = Converters.ConvertToOwnerPw(ownerDto);

        var ownerCreated = await _ownerRepository.CreateOwner(ow1);
        if (!ownerCreated)
        {
            return Result.Failure<OwnerDTOCreate>("Owner could not be saved (already exists)!");
        }

        return Result.Success(ownerDto);
    }

    public async Task<Result<OwnerDTO>> UpdateOwner(int oldOwnerId, OwnerDTO newOwnerDto)
    {
        var ownerToUpdate = await _ownerRepository.GetOwnerById(oldOwnerId);
        if (ownerToUpdate == null)
        {
            return Result.Failure<OwnerDTO>("The owner you want to update was not found!");
        }

        var newOwner = Converters.ConvertToOwner(newOwnerDto);

        var ownerFieldsAlreadyUsed = await _ownerRepository.OwnerExists(newOwner.Id, newOwner.VAT, newOwner.Email, newOwner.PhoneNumber);

        if(ownerFieldsAlreadyUsed)
        {
            return Result.Failure<OwnerDTO>("Update failed (duplicated info with existing owner).");
        }

        ownerToUpdate = Clone(ownerToUpdate, newOwner);
        var ownerUpdated = await _ownerRepository.UpdateOwner(ownerToUpdate);

        if (!ownerUpdated)
        {
            return Result.Failure<OwnerDTO>("Update failed!");
        }

        return Result.Success(newOwnerDto);
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

    public async Task<Result<List<OwnerDTO>>> SearchOwner(string vat, string email)
    {
        var owners = await _ownerRepository.Search(vat, email);
        if(owners == null)
        {
            return Result.Failure<List<OwnerDTO>>("No owners found with the specified criteria.");
        }

        var ownersDTO = owners.Select(Converters.ConvertToOwnerDTO).ToList();

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
}
