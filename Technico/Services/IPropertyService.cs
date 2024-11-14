

using TechnicoWebApi.Dtos;
using TechnicoLibrary.Models;

namespace Technico.Services;

public interface IPropertyService 
{ 
    Task<List<PropertyDto>> GetProperties();
    Task<PropertyDto> GetPropertyById(int id);
    Task<List<PropertyDto>> GetPropertiesByOwnerId(int id);
    Task<bool> CreateProperty(PropertyDto propertyDto, int ownerId);
    Task<bool> DeleteProperty(int id);
}
