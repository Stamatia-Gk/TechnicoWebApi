// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Data;
using Technico.DTO;
using Technico.Models;
using Technico.Repositories.Implementations;
using Technico.Repositories.Interfaces;
using Technico.Services.Interfaces;
using Technico.Validator;

namespace Technico.Services.Implementations;

public class RepairService : IRepairService
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IRepairRepository _repairRepository;
    private readonly RepairValidator _repairValidator;

    public RepairService(IOwnerRepository ownerRepository, IRepairRepository repairRepository, RepairValidator repairValidator)
    {
        _ownerRepository = ownerRepository;
        _repairRepository = repairRepository;
        _repairValidator = repairValidator;
    }

    public async Task<Result<RepairDTO>> CreateRepair(RepairDTO repairDto, int ownerId)
    {
        //if (!(await _repairValidator.ValidateAsync(repairDto)).IsValid)
        //{
        //    return Result.Failure<RepairDTO>("Invalid input");
        //}

        var owner = await _ownerRepository.GetOwner(ownerId);

        if (owner == null) 
        {
            return Result.Failure<RepairDTO>("Owner does not exist");
        }

        Repair newRepair = Converters.ConvertToRepair(repairDto);

        var repairCreated = await _repairRepository.CreateRepair(newRepair, owner);
        if (!repairCreated)
        {
            return Result.Failure<RepairDTO>("Failed to create repair");
        }
       
        RepairDTO createdRepairDto = Converters.ConvertToRepairDTO(newRepair);
        
        return Result.Success(createdRepairDto);
    }

    public async Task<Result<RepairDTO>> GetRepair(int id)
    {
        var repair = await _repairRepository.GetRepair(id);
        if (repair == null)
        {
            return Result.Failure<RepairDTO>("Repair not found");
        }
        return Result.Success(Converters.ConvertToRepairDTO(repair));
    }

    public async Task<Result<List<RepairDTO>>> GetAllRepairs()
    {
        var repairs = await _repairRepository.GetRepairs();
        if(repairs == null || repairs.Any())
        {
            return Result.Failure<List<RepairDTO>>("No repairs found");
        }
        var repairDTOs = repairs.Select(Converters.ConvertToRepairDTO).ToList();
        return Result.Success(repairDTOs);
    }

    public async Task<Result<RepairDTO>> UpdateRepair(int oldRepairId, RepairDTO newRepairDto)
    {
        var repairToUpdate = await _repairRepository.GetRepair(oldRepairId);
        if (repairToUpdate == null)
        {
            return Result.Failure<RepairDTO>("The repair you want to update was not found");
        }

        Repair repair1 = Converters.ConvertToRepair(newRepairDto);
        repairToUpdate = repair1;

        var repairUpdated = await _repairRepository.UpdateRepair(repairToUpdate);

        if (!repairUpdated)
        {
            return Result.Failure<RepairDTO>("Update failed");
        }
        return Result.Success(newRepairDto);
    }

    public async Task<Result> DeleteRepair(int repairId)
    {
        var repairToDelete = await _repairRepository.GetRepair(repairId);
        if (repairToDelete == null)
        {
            return Result.Failure("This repair does not exist");
        }

        var repairDeleted = await _repairRepository.DeleteRepair(repairToDelete);

        return repairDeleted ? Result.Success("Repair successfully deleted") : Result.Failure("Delete failed");
    }
}
