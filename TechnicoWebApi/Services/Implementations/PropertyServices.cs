// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;
using Technico.Models;
using Technico.Repositories.Interfaces;
using Technico.Services.Interfaces;

namespace TechnicoWebApi.Services.Implementations;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<List<PropertyDTO>>> GetAllProperties()
    {
        var propertiesList = await _propertyRepository.GetProperties();

        var propertiesListDto =  propertiesList.Select(p => Converters.ConvertToPropertyDTO(p)).ToList();

        return Result.Success(propertiesListDto);
    }

    public async Task<Result<PropertyDTO>> GetPropertyById(int id)
    {
        var property = await _propertyRepository.GetPropertyById(id);
        if (property == null)
        {
            return Result.Failure<PropertyDTO>("Property Not Found");
        }

        var propertyDto = Converters.ConvertToPropertyDTO(property);

        return Result.Success(propertyDto);
    }

    public async Task<Result<List<PropertyDTO>>> GetAllPropertiesOfAnOwner(int ownerId)
    {
        var propertyList = await _propertyRepository.GetPropertiesByOwnerId(ownerId);
        if (propertyList.Count == 0)
        {
            return Result.Failure<List<PropertyDTO>>("No properties found for this owner");
        }

        var propertyListDto = propertyList.Select(p => Converters.ConvertToPropertyDTO(p)).ToList();

        return Result.Success(propertyListDto);
    }

    public async Task<Result<PropertyDTO>> CreateProperty(PropertyDTO propertyDto, int ownerId)
    {
        var propertyToCreate = Converters.ConvertToPropertyItem(propertyDto);
        var propertyCreated = await _propertyRepository.CreateProperty(propertyToCreate, ownerId);
        if (!propertyCreated)
        {
            return Result.Failure<PropertyDTO>("Owners not found");
        }
        
        return Result.Success(propertyDto);
    }

    public async Task<Result<PropertyDTO>> UpdateProperty(int oldPropertyId, PropertyDTO propertyDto)
    {
        var propertyToUpdate = await _propertyRepository.GetPropertyById(oldPropertyId);
        if (propertyToUpdate == null)
        {
            return Result.Failure<PropertyDTO>("The property you want to update was not found");
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
            return Result.Failure<PropertyDTO>("Update failed");
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

    public async Task<Result<List<PropertyDTO>>> SearchProperty(int propertyId, string ownerVat)
    {
        var propertyList = await _propertyRepository.Search(propertyId, ownerVat);
        if ( propertyList.Count == 0)
        {
            return Result.Failure<List<PropertyDTO>>("No properties found for this search");
        }

        var propertyListDto = propertyList.Select(p => Converters.ConvertToPropertyDTO(p)).ToList();
        
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
