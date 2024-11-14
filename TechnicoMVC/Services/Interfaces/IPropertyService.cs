// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using TechnicoWebApi.Dtos;

namespace Technico.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<Result<List<PropertyDto>>> GetAllProperties();
        Task<Result<PropertyDto>> GetPropertyById(int id);
        Task<Result<List<PropertyDto>>> GetAllPropertiesOfAnOwner(int ownerId);
        Task<Result<PropertyDto>> CreateProperty(PropertyDto propertyDto, int ownerId);
        Task<Result<PropertyDto>> UpdateProperty(int oldPropertyId, PropertyDto propertyDto);
        Task<Result> DeleteProperty(int propertyId);
        Task<Result<List<PropertyDto>>> SearchProperty(int propertyId, string ownerVat);
    }
}