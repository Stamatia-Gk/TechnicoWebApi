

using TechnicoWebApi.Dtos;

namespace Technico.Services;

public interface IPropertyService 
{ 
    Task<List<PropertyDto>> GetProperties();
}
