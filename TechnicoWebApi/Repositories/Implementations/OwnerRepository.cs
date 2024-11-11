// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore;
using Technico.Data;
using Technico.Models;
using Technico.Repositories.Interfaces;

namespace Technico.Repositories.Implementations;

public class OwnerRepository : IOwnerRepository
{
    private readonly TechnicoDbContext _context;

    public OwnerRepository(TechnicoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Owner>> GetOwners()
    {
        return await _context.Owners.OrderBy(o => o.Surname).ToListAsync();
    }

    public async Task<Owner?> GetOwnerById(int id)
    {
        return await _context.Owners.Where(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<PropertyItem>> GetOwnerProperties(int id)
    {
        var allOwnerRepairs = await _context.Owners
                                .Include(p => p.Properties)
                                .Where(o => o.Id == id)
                                .Select(o => o.Properties)
                                .SingleOrDefaultAsync();
        return allOwnerRepairs;
    }

    public async Task<List<Repair>> GetOwnerRepairs(int id)
    {
        var allOwnerRepairs = await _context.Owners
                                .Include(r => r.AllRepairs)
                                .Where(o => o.Id == id)
                                .Select(o => o.AllRepairs)
                                .SingleOrDefaultAsync();
        return allOwnerRepairs;
    }

    public async Task<bool> CreateOwner(Owner owner)
    {
        if (!OwnerExists(owner.Id, owner.VAT, owner.Email, owner.PhoneNumber).Result)
        {
            _context.Add(owner);
        }

        return await Save();
    }

    public async Task<bool> UpdateOwner(Owner owner)
    {
        _context.ChangeTracker.Clear();
        _context.Update(owner);
        return await Save();
    }

    public async Task<bool> DeleteOwner(Owner owner)
    {
        if (OwnerExists(owner.Id, owner.VAT, owner.Email, owner.PhoneNumber).Result)
        {
            _context.Remove(owner);
        }

        return await Save();
    }

    public async Task<bool> OwnerExists(int id, string vatNumber, string email, string phone)
    {
        var ownerFound = await _context.Owners.AnyAsync(o => o.VAT.Equals(vatNumber.Trim())
                                                     || o.Email.Equals(email.Trim())
                                                     || o.PhoneNumber.Equals(phone.Trim())
                                                     && o.Id != id);
        return ownerFound;
    }

    public async Task<bool> Save()
    {
        var saved = _context.SaveChangesAsync();
        return await saved > 0;
    }
}
