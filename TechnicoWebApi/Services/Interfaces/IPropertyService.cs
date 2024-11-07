// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;
using Technico.Models;

namespace Technico.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<Result<PropertyDTO>> CreateProperty(PropertyItem property, List<string> ownersVatNumbers);
        Task<Result> DeleteProperty(int propertyId);
        Task<Result<PropertyDTO>> GetProperty(int id);
        Task<Result<PropertyDTO>> UpdateProperty(int oldPropertyId, PropertyItem newProperty);
    }
}