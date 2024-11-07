// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Technico.Data;
using Technico.DTO;
using Technico.Models;
using Technico.Repositories.Interfaces;
using Technico.Services.Interfaces;
using Technico.Validator;

namespace Technico.Services.Implementations;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly PropertyValidator _propertyValidator;
    public PropertyService(IPropertyRepository propertyRepository, PropertyValidator propertyValidator)
    {
        _propertyRepository = propertyRepository;
        _propertyValidator = propertyValidator;
    }
    public async Task<Result<PropertyDTO>> CreateProperty(PropertyItem property, List<string> ownersVatNumbers)
    {
        if (!(await _propertyValidator.ValidateAsync(property)).IsValid)
        {
            return Result.Failure<PropertyDTO>("Invalid input");
        }
        var propertyCreated = await _propertyRepository.CreateProperty(property, ownersVatNumbers);
        if (!propertyCreated)
        {
            return Result.Failure<PropertyDTO>("Owners not found");
        }

        var propertyDTO = MapToPropertyDTO(property);

        return Result.Success(propertyDTO);
    }

    public async Task<Result<PropertyDTO>> GetProperty(int id)
    {
        var property = await _propertyRepository.GetProperty(id);
        if (property == null)
        {
            return Result.Failure<PropertyDTO>("Property Not Found");
        }

        var propertyDTO = MapToPropertyDTO(property);

        return Result.Success(propertyDTO);
    }

    public async Task<Result<PropertyDTO>> UpdateProperty(int oldPropertyId, PropertyItem newProperty)
    {
        var propertyToUpdate = await _propertyRepository.GetProperty(oldPropertyId);
        if (propertyToUpdate == null)
        {
            return Result.Failure<PropertyDTO>("The property you want to update was not found");
        }
        var oldOwners = propertyToUpdate.Owners;
        propertyToUpdate = Clone(propertyToUpdate, newProperty);

        if (propertyToUpdate.Owners.Count == 0)
        {
            propertyToUpdate.Owners = oldOwners;
        }

        var propertyUpdated = await _propertyRepository.UpdateProperty(propertyToUpdate);

        if (!propertyUpdated)
        {
            return Result.Failure<PropertyDTO>("Update failed");
        }

        var propertyDTO = MapToPropertyDTO(propertyToUpdate);
        return Result.Success(propertyDTO);
    }

    public async Task<Result> DeleteProperty(int propertyId)
    {

        var propertyToDelete = await _propertyRepository.GetProperty(propertyId);
        if (propertyToDelete == null)
        {
            return Result.Failure("This property does not exist");
        }

        var ownerDeleted = await _propertyRepository.DeleteProperty(propertyToDelete);

        return ownerDeleted ? Result.Success("Property successfully deleted") : Result.Failure("Delete failed");
    }

    private PropertyDTO MapToPropertyDTO(PropertyItem property) // DTO
    {
        var propertyDTO = new PropertyDTO(property.Id, property.IdentificationNumber, property.Address,
            property.ConstructionYear, property.PropertyType);

        return propertyDTO;
    }

    private static PropertyItem Clone(PropertyItem oldProperty, PropertyItem newProperty)
    {
        oldProperty.Address = newProperty.Address;
        oldProperty.ConstructionYear = newProperty.ConstructionYear;
        oldProperty.IdentificationNumber = newProperty.IdentificationNumber;
        oldProperty.Owners = newProperty.Owners;
        oldProperty.PropertyType = newProperty.PropertyType;

        return oldProperty;
    }
}
