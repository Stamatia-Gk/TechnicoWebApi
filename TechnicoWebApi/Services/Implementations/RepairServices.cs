// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.DTO;
using Technico.Models;
using Technico.Repositories.Interfaces;
using Technico.Services.Interfaces;
using Technico.Validator;

namespace TechnicoWebApi.Services.Implementations;

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
        var owner = await _ownerRepository.GetOwnerById(ownerId);

        if (owner == null)
        {
            return Result.Failure<RepairDTO>("Owner does not exist");
        }

        Repair newRepair = Converters.ConvertToRepair(repairDto);
        newRepair.Owner = owner;
        var repairCreated = await _repairRepository.CreateRepair(newRepair);
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
        if (repairs == null)
        {
            return Result.Failure<List<RepairDTO>>("No repairs found");
        }
        var repairDTOs = repairs.Select(Converters.ConvertToRepairDTO).ToList();
        return Result.Success(repairDTOs);
    }

    public async Task<Result<List<RepairDTO>>> SearchRepair(DateTime startDate, DateTime endDate, int userId)
    {
        if (endDate < startDate)
        {
            return Result.Failure<List<RepairDTO>>("End date must be greater or equal than the start date");
        }
        var owner = await _ownerRepository.GetOwnerById(userId);

        if (owner == null)
        {
            return Result.Failure<List<RepairDTO>>("Owner does not exist");
        }
        var repairs = await _repairRepository.Search(startDate, endDate, owner.Id);

        if (repairs == null)
        {
            return Result.Failure<List<RepairDTO>>("No repairs found with the specified criteria.");
        }

        var repairsDTO = repairs.Select(Converters.ConvertToRepairDTO).ToList();

        return Result.Success(repairsDTO);
    }
    public async Task<Result<RepairDTO>> UpdateRepair(int oldRepairId, RepairDTO newRepairDto)
    {
        
        var repairToUpdate = await _repairRepository.GetRepair(oldRepairId);
        if (repairToUpdate == null)
        {
            return Result.Failure<RepairDTO>("The repair you want to update was not found");
        }

        Repair newRepair = Converters.ConvertToRepair(newRepairDto);

        if (repairToUpdate.Owner != null)
        {
            newRepair.Owner = repairToUpdate.Owner;
        }
        
        repairToUpdate = Clone(repairToUpdate, newRepair);

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

    private static Repair Clone(Repair oldRepair, Repair newRepair)
    {
        oldRepair.Cost = newRepair.Cost;
        oldRepair.Description = newRepair.Description;
        oldRepair.Address = newRepair.Address;
        oldRepair.RepairStatus = newRepair.RepairStatus;
        oldRepair.RepairType = newRepair.RepairType;
        oldRepair.ScheduledRepair = newRepair.ScheduledRepair;

        return oldRepair;
    }
}