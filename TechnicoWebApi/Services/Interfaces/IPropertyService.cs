// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;
using Technico.Models;

namespace Technico.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<Result<List<PropertyDTO>>> GetAllProperties();
        Task<Result<PropertyDTO>> CreateProperty(PropertyDTO propertyDto, int ownerId);
        Task<Result<PropertyDTO>> GetProperty(int id);
        Task<Result<PropertyDTO>> UpdateProperty(int oldPropertyId, PropertyDTO propertyDto);
        Task<Result> DeleteProperty(int propertyId);
    }
}