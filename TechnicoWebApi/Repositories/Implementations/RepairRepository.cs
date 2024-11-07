// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Data;
using Technico.Models;
using Technico.Repositories.Interfaces;

namespace Technico.Repositories.Implementations;

public class RepairRepository : IRepairRepository
{
    private readonly TechnicoDbContext _context;
    private readonly IOwnerRepository _ownerRepository;

    public RepairRepository(TechnicoDbContext context, IOwnerRepository ownerRepository)
    {
        _context = context;
        _ownerRepository = ownerRepository;
    }

    public async Task<List<Repair>> GetRepairs()
    {
        return await _context.Repairs.OrderBy(r => r.ScheduledRepair).ToListAsync();
    }

    public async Task<Repair?> GetRepair(int id)
    {
        return await _context.Repairs.Where(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> RepairExists(int id)
    {
        return await _context.Repairs.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> CreateRepair(Repair repair, Owner owner)
    {
        var ownerExists = await _ownerRepository.OwnerExists(owner.VAT);
        if (!ownerExists)
        {
            return false;
        }

        repair.Owner = owner;
        _context.Add(repair);

        return await Save();
    }

    public async Task<bool> UpdateRepair(Repair repair)
    {
        _context.ChangeTracker.Clear();
        _context.Update(repair);
        return await Save();
    }

    public Task<bool> DeleteRepair(Repair repair)
    {
        _context.Remove(repair);
        return Save();
    }

    public async Task<bool> Save()
    {
        var saved = _context.SaveChangesAsync();
        return await saved > 0;
    }
}
