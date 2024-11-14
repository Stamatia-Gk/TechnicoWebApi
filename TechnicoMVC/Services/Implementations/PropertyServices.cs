// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.Services.Interfaces;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;
using TechnicoWebApi.Repositories.Interfaces;

namespace TechnicoWebApi.Services.Implementations;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<List<PropertyDto>>> GetAllProperties()
    {
        var propertiesList = await _propertyRepository.GetProperties();

        var propertiesListDto =  propertiesList.Select(p => Converters.ConvertToPropertyDto(p)).ToList();

        return Result.Success(propertiesListDto);
    }

    public async Task<Result<PropertyDto>> GetPropertyById(int id)
    {
        var property = await _propertyRepository.GetPropertyById(id);
        if (property == null)
        {
            return Result.Failure<PropertyDto>("Property Not Found");
        }

        var propertyDto = Converters.ConvertToPropertyDto(property);
        return Result.Success(propertyDto);
    }

    public async Task<Result<List<PropertyDto>>> GetAllPropertiesOfAnOwner(int ownerId)
    {
        var propertyList = await _propertyRepository.GetPropertiesByOwnerId(ownerId);
        if (propertyList.Count == 0)
        {
            return Result.Failure<List<PropertyDto>>("No properties found for this owner");
        }

        var propertyListDto = propertyList.Select(p => Converters.ConvertToPropertyDto(p)).ToList();
        return Result.Success(propertyListDto);
    }

    public async Task<Result<PropertyDto>> CreateProperty(PropertyDto propertyDto, int ownerId)
    {
        var propertyToCreate = Converters.ConvertToPropertyItem(propertyDto);
        var propertyCreated = await _propertyRepository.CreateProperty(propertyToCreate, ownerId);
        if (!propertyCreated)
        {
            return Result.Failure<PropertyDto>("Owners not found");
        }
        
        return Result.Success(propertyDto);
    }

    public async Task<Result<PropertyDto>> UpdateProperty(int oldPropertyId, PropertyDto propertyDto)
    {
        var propertyToUpdate = await _propertyRepository.GetPropertyById(oldPropertyId);
        if (propertyToUpdate == null)
        {
            return Result.Failure<PropertyDto>("The property you want to update was not found");
        }
       
        var oldOwners = propertyToUpdate.Owners;
        var newProperty = Converters.ConvertToPropertyItem(propertyDto);
        propertyToUpdate = Clone(propertyToUpdate, newProperty);

        if (propertyToUpdate.Owners.Count == 0)
        {
            propertyToUpdate.Owners = oldOwners;
        }

        var propertyUpdated = await _propertyRepository.UpdateProperty(propertyToUpdate);

        if (!propertyUpdated)
        {
            return Result.Failure<PropertyDto>("Update failed");
        }
        
        return Result.Success(propertyDto);
    }

    public async Task<Result> DeleteProperty(int propertyId)
    {

        var propertyToDelete = await _propertyRepository.GetPropertyById(propertyId);
        if (propertyToDelete == null)
        {
            return Result.Failure("This property does not exist");
        }

        var ownerDeleted = await _propertyRepository.DeleteProperty(propertyToDelete);
        return ownerDeleted ? Result.Success("Property successfully deleted") : Result.Failure("Delete failed");
    }

    public async Task<Result<List<PropertyDto>>> SearchProperty(int propertyId, string ownerVat)
    {
        var propertyList = await _propertyRepository.Search(propertyId, ownerVat);
        if ( propertyList.Count == 0)
        {
            return Result.Failure<List<PropertyDto>>("No properties found for this search");
        }

        var propertyListDto = propertyList.Select(p => Converters.ConvertToPropertyDto(p)).ToList();
        return Result.Success(propertyListDto);
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
