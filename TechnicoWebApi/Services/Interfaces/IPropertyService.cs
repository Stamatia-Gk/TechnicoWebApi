// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;

namespace Technico.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<Result<List<PropertyDTO>>> GetAllProperties();
        Task<Result<PropertyDTO>> GetPropertyById(int id);
        Task<Result<List<PropertyDTO>>> GetAllPropertiesOfAnOwner(int ownerId);
        Task<Result<PropertyDTO>> CreateProperty(PropertyDTO propertyDto, int ownerId);
        Task<Result<PropertyDTO>> UpdateProperty(int oldPropertyId, PropertyDTO propertyDto);
        Task<Result> DeleteProperty(int propertyId);
        Task<Result<List<PropertyDTO>>> SearchProperty(int propertyId, string ownerVat);
    }
}