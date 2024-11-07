// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;

namespace Technico.Repositories.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<PropertyItem>> GetProperties();
        Task<PropertyItem?> GetProperty(int id);
        Task<bool> PropertyExists(int id);
        Task<bool> CreateProperty(PropertyItem property, List<string> propertyOwnersVatNumbers);
        Task<bool> UpdateProperty(PropertyItem property);
        Task<bool> DeleteProperty(PropertyItem property);
        Task<bool> Save();
    }
}