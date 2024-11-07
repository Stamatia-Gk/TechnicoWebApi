// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Data;
using Technico.DTO;
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

    public async Task<bool> CreateOwner(Owner owner)
    {
        if (!OwnerExists(owner.VAT).Result)
        {
            _context.Add(owner);
        }

        return await Save();
    }

    public async Task<List<Owner>> GetOwners()
    {
        return await _context.Owners.OrderBy(o => o.Surname).ToListAsync();
    }

    public async Task<bool> UpdateOwner(Owner owner)
    {
        _context.ChangeTracker.Clear();
        _context.Update(owner);
        return await Save();
    }

    public async Task<bool> DeleteOwner(Owner owner)
    {
        if (OwnerExists(owner.VAT).Result)
        {
            _context.Remove(owner);
        }

        return await Save();
    }

    public async Task<Owner?> GetOwner(int id)
    {
        return await _context.Owners.Where(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> OwnerExists(string vatNumber)
    {
        return await _context.Owners.AnyAsync(o => o.VAT.Equals(vatNumber.Trim()));
    }

    public async Task<bool> Save()
    {
        var saved = _context.SaveChangesAsync();
        return await saved > 0;
    }
}
