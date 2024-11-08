// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Data;
using Technico.Models;
using Technico.Repositories.Interfaces;

namespace Technico.Repositories.Implementations;

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

    public async Task<PropertyItem?> GetProperty(int id)
    {
        return await _context.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> PropertyExists(int id)
    {
        return await _context.Properties.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> CreateProperty(PropertyItem property, string ownerVatNumber)
    {
        var owner = await _context.Owners.Where(o => o.VAT.Trim().Equals(ownerVatNumber)).FirstOrDefaultAsync();
        if (owner == null) return false;
        
        property.Owners.Add(owner);

        if (!PropertyExists(ownerVatNumber).Result)
        {
            _context.Properties.Add(property);
        }

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

    public async Task<bool> Save()
    {
        var saved = _context.SaveChangesAsync();
        return await saved > 0;
    }

    public async Task<bool> PropertyExists(string identificationNumber)
    {
        return await _context.Properties.AnyAsync(p => p.IdentificationNumber.Trim().Equals(identificationNumber));
    }
}
