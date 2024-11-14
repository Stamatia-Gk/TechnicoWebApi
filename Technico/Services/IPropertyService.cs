

using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

namespace Technico.Services;

public interface IPropertyService 
{ 
    Task<List<PropertyDto>> GetProperties();
    Task<bool> CreateProperty(PropertyDto propertyDto, int ownerId);
    Task<PropertyDto> GetPropertyById(int id);
    Task<List<PropertyDto>> GetPropertiesByOwnerId(int id);
    Task<bool> DeleteProperty(int id);
}
