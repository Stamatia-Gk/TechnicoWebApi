﻿// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore;
using Technico.Data;
using Technico.Models;
using TechnicoWebApi.Models;
using TechnicoWebApi.Repositories.Interfaces;

namespace TechnicoWebApi.Repositories.Implementations;

public class RepairRepository : IRepairRepository
{
    private readonly TechnicoDbContext _context;

    public RepairRepository(TechnicoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Repair>> GetRepairs()
    {
        return await _context.Repairs.OrderBy(r => r.ScheduledRepair).ToListAsync();
    }

    public async Task<List<Repair>> GetRepairsInProgress()
    {
        return await _context.Repairs.Where(r => r.RepairStatus == RepairStatus.InProgress).ToListAsync();
    }

    public async Task<Repair?> GetRepairById(int id)
    {
        return await _context.Repairs.Where(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Repair?>> GetRepairsByOwnerId(int id)
    {
        return await _context.Repairs.Where(r => r.Owner.Id == id).ToListAsync();
    }

    public async Task<bool> CreateRepair(Repair repair)
    {
        _context.Repairs.Add(repair);
        return await Save();
    }

    public async Task<bool> UpdateRepair(Repair repair)
    {
        var existingOwner = await _context.Owners.FindAsync(repair.Owner.Id);
        repair.Owner = existingOwner;
        _context.ChangeTracker.Clear();
        _context.Update(repair);
        return await Save();
    }

    public Task<bool> DeleteRepair(Repair repair)
    {
        _context.Remove(repair);
        return Save();
    }

    public async Task<List<Repair>> Search(DateTime startDate, DateTime endDate, int ownerId=0)
    {
        if (startDate == null && endDate == null && ownerId == 0)
        {
            return null;
        }
        if ((startDate != null || endDate == null) && ownerId != 0)
        {
            return await _context.Repairs.Where(r => r.Owner.Id == ownerId).ToListAsync();
        }

        if (startDate != null && endDate != null && ownerId == 0)
        {
            return await _context.Repairs.Where(r => r.ScheduledRepair >= startDate && r.ScheduledRepair <= endDate).ToListAsync();
        }

        return await _context.Repairs.Where(r => r.Owner.Id == ownerId
        && r.ScheduledRepair >= startDate
        && r.ScheduledRepair <= endDate)
            .ToListAsync();
    }

    public async Task<bool> RepairExists(int id)
    {
        return await _context.Repairs.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> Save()
    {
        var saved = _context.SaveChangesAsync();
        return await saved > 0;
    }
}
