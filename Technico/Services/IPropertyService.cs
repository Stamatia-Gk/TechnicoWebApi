

using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

namespace Technico.Services;

public interface IPropertyService 
{    
    Task<bool> CreateProperty(PropertyDto propertyDto, int ownerId);
    Task<List<PropertyDto>> GetProperties();

    Task<List<PropertyDto>> SearchPropertiesByOwnerOrVatAsync(int id = 0, string vat = null);
    Task<PropertyDto> GetPropertyById(int id);
    Task<List<PropertyDto>> GetPropertiesByOwnerId(int id);
    Task<PropertyDto> UpdateProperty(PropertyDto repairDto, int id);
    Task<List<PropertyDto>> DeleteProperty(int id);
}
