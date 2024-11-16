// Team Project | European Dynamics | Code.Hub Project 2024

using Microsoft.EntityFrameworkCore;
using Technico.Data;
using TechnicoWebApi.Models;
using TechnicoWebApi.Repositories.Interfaces;

namespace TechnicoWebApi.Repositories.Implementations;

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

    public async Task<Owner?> GetOwnerByVat(string vat)
    {
        return await _context.Owners.Where(o => o.VAT == vat).FirstOrDefaultAsync();
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
        _context.Remove(owner);
        return await Save();
    }

    public async Task<Owner> Search(string? vat, string? email)
    {
        if (vat == null && email == null)
        {
            return null;
        }

        if (vat == null)
        {
            return await _context.Owners.Where(o => o.Email == email).FirstOrDefaultAsync();
        }

        if (email == null)
        {
            return await _context.Owners.Where(o => o.VAT == vat).FirstOrDefaultAsync();
        }
        
        return await _context.Owners.Where(o => o.VAT == vat && o.Email == email).FirstOrDefaultAsync();
    }

    public async Task<bool> OwnerExists(int id, string vatNumber, string email, string phone)
    {   
        var ownerFound = await _context.Owners.AnyAsync(o => 
            (o.VAT.Equals(vatNumber.Trim()) || o.Email.Equals(email.Trim()) || o.PhoneNumber.Equals(phone.Trim())) 
            && o.Id != id);
        return ownerFound;
    }

    public async Task<bool> Save()
    {
        var saved = _context.SaveChangesAsync();
        return await saved > 0;
    }

    public async Task<Owner> Login(string email, string password)
    {
        if(email == null || password == null)
        { 
            return null; 
        }

        var owner = await _context.Owners.Where(o => o.Email == email && o.Password == password).FirstOrDefaultAsync();
        return owner;
    }
}
