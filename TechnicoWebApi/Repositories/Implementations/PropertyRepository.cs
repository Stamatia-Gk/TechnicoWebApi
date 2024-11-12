// Team Project | European Dynamics | Code.Hub Project 2024

using Microsoft.EntityFrameworkCore;
using Technico.Data;
using TechnicoWebApi.Models;
using TechnicoWebApi.Repositories.Interfaces;

namespace TechnicoWebApi.Repositories.Implementations;

public class PropertyRepository : IPropertyRepository
{
    private readonly TechnicoDbContext _context;

    public PropertyRepository(TechnicoDbContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyItem>> GetProperties()
    {
        return await _context.Properties.OrderBy(p => p.ConstructionYear).ToListAsync();
    }
    public async Task<List<PropertyItem>?> GetPropertiesByOwnerId(int id)
    {
        return await _context.Properties.Where(p => p.Owners.Any(o => o.Id == id)).ToListAsync();
    }

    public async Task<PropertyItem?> GetPropertyById(int id)
    {
        return await _context.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> CreateProperty(PropertyItem property, int ownerId)
    {
        var owner = await _context.Owners.Where(o => o.Id == ownerId).FirstOrDefaultAsync();
        if (owner == null) return false;

        var fetchedProperty = await GetPropertyByIdentNum(property.IdentificationNumber);
        if (fetchedProperty == null)
        {
            property.Owners.Add(owner);
            _context.Add(property);
            return await Save();
        }

        var ownerInProperty = fetchedProperty.Owners.Any(o => o.Id == ownerId);
        if (ownerInProperty)
        {
            return false;
        }

        fetchedProperty.Owners.Add(owner);
        return await Save();
    }

    public async Task<bool> UpdateProperty(PropertyItem property)
    {
        _context.Update(property);
        return await Save();
    }

    public async Task<bool> DeleteProperty(PropertyItem property)
    {
        if (PropertyExists(property.Id).Result)
        {
            _context.Remove(property);
        }

        return await Save();
    }

    public async Task<List<PropertyItem>>? Search(int propertyId, string? ownerVat)
    {
        if (propertyId == 0 && ownerVat == null )
        {
            return new List<PropertyItem>();
        }
        if (propertyId == 0 )
        {
            return await _context.Properties.Where(p => p.Owners.Any(o => o.VAT == ownerVat)).ToListAsync();
        } 
        if (ownerVat == null)
        {
            return await _context.Properties.Where(p => p.Id == propertyId).ToListAsync();
        }
        
        return await _context.Properties.Where(p => p.Owners.Any(o => o.VAT == ownerVat) && p.Id == propertyId).ToListAsync();
        
    }

    public async Task<bool> PropertyExists(int id)
    {
        return await _context.Properties.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> Save()
    {
        var saved = _context.SaveChangesAsync();
        Console.WriteLine(saved);
        return await saved > 0;
    }
    
    public async Task<PropertyItem?> GetPropertyByIdentNum(string IdentNum) // Do we want that for sure since we're working with IDs?
    {
        return await _context.Properties.Where(p => p.IdentificationNumber.Equals(IdentNum)).Include(p => p.Owners).FirstOrDefaultAsync();
    }

    public async Task<bool> PropertyExists(string identificationNumber) // Do we want that for sure since we're working with IDs?
    {
        return await _context.Properties.AnyAsync(p => p.IdentificationNumber.Trim().Equals(identificationNumber));
    }

  
}
