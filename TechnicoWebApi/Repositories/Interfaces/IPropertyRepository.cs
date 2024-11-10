// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;

namespace Technico.Repositories.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<PropertyItem>> GetProperties();
        Task<PropertyItem?> GetPropertyById(int id);
        Task<bool> CreateProperty(PropertyItem property, int ownerId);
        Task<bool> UpdateProperty(PropertyItem property);
        Task<bool> DeleteProperty(PropertyItem property);
        Task<bool> PropertyExists(int id);
        Task<bool> Save();
    }
}