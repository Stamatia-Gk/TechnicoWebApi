// Team Project | European Dynamics | Code.Hub Project 2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Data;
using Technico.Models;
using Technico.Repositories.Interfaces;
using Technico.Services.Interfaces;
using Technico.Validator;
using CSharpFunctionalExtensions;
using Technico.DTO;
using FluentValidation;

namespace Technico.Services.Implementations;
public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
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

    
    public async Task<Result<OwnerDTO>> GetOwner(int id)
    {
        var owner = await _ownerRepository.GetOwner(id);
        if (owner == null)
        {
            return Result.Failure<OwnerDTO>("Owner not found!");
        }

        return Result.Success(Converters.ConvertToOwnerDTO(owner));
    }

    public async Task<Result<List<OwnerDTO>>> GetAllOwners()
    {
        var ownersList = await _ownerRepository.GetOwners();
        var ownerDtos = ownersList.Select(owner => Converters.ConvertToOwnerDTO(owner));
        return ownerDtos.ToList();
    }

    public async Task<Result<OwnerDTO>> UpdateOwner(int oldOwnerId, OwnerDTO newOwnerDto)
    {
        var ownerToUpdate = await _ownerRepository.GetOwner(oldOwnerId);
        if (ownerToUpdate == null)
        {
            return Result.Failure<OwnerDTO>("The owner you want to update was not found!");
        }

        Owner ow1 = Converters.ConvertToOwner(newOwnerDto);

        ownerToUpdate = ow1;

        var ownerUpdated = await _ownerRepository.UpdateOwner(ownerToUpdate);

        if (!ownerUpdated)
        {
            return Result.Failure<OwnerDTO>("Update failed!");
        }

        return Result.Success(newOwnerDto);
    }

    public async Task<Result> DeleteOwner(int ownerId)
    {
        var ownerToDelete = await _ownerRepository.GetOwner(ownerId);

        if (ownerToDelete == null)
        {
            return Result.Failure("This owner does not exist");
        }

        var ownerDeleted = await _ownerRepository.DeleteOwner(ownerToDelete);
        return ownerDeleted ? Result.Success("Owner successfully deleted.") : Result.Failure("Delete failed.");
    }
}