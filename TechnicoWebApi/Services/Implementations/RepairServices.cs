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

    public async Task<Result<RepairDTOEmployee>> CreateRepair(RepairDTOEmployee repairDto, int ownerId)
    {
        var owner = await _ownerRepository.GetOwnerById(ownerId);

        if (owner == null)
        {
            return Result.Failure<RepairDTOEmployee>("Owner does not exist");
        }

        Repair newRepair = Converters.ConvertToRepairEmployee(repairDto);
        newRepair.Owner = owner;
        var repairCreated = await _repairRepository.CreateRepair(newRepair);
        if (!repairCreated)
        {
            return Result.Failure<RepairDTOEmployee>("Failed to create repair");
        }

        RepairDTOEmployee createdRepairDto = Converters.ConvertToRepairDTOEmployee(newRepair);

        return Result.Success(createdRepairDto);
    }

    public async Task<Result<RepairDTOEmployee>> GetRepair(int id)
    {
        var repair = await _repairRepository.GetRepairById(id);
        if (repair == null)
        {
            return Result.Failure<RepairDTOEmployee>("Repair not found");
        }
        return Result.Success(Converters.ConvertToRepairDTOEmployee(repair));
    }

    public async Task<Result<List<RepairDTOEmployee>>> GetAllRepairs()
    {
        var repairs = await _repairRepository.GetRepairs();
        if (repairs == null)
        {
            return Result.Failure<List<RepairDTOEmployee>>("No repairs found");
        }
        var repairDTOs = repairs.Select(Converters.ConvertToRepairDTOEmployee).ToList();
        return Result.Success(repairDTOs);
    }

    public async Task<Result<List<RepairDTOEmployee>>> SearchRepair(DateTime startDate, DateTime endDate, int userId)
    {
        if (endDate < startDate)
        {
            return Result.Failure<List<RepairDTOEmployee>>("End date must be greater or equal than the start date");
        }
        var owner = await _ownerRepository.GetOwnerById(userId);

        if (owner == null)
        {
            return Result.Failure<List<RepairDTOEmployee>>("Owner does not exist");
        }
        var repairs = await _repairRepository.Search(startDate, endDate, owner.Id);

        if (repairs == null)
        {
            return Result.Failure<List<RepairDTOEmployee>>("No repairs found with the specified criteria.");
        }

        var repairsDTO = repairs.Select(Converters.ConvertToRepairDTOEmployee).ToList();

        return Result.Success(repairsDTO);
    }
    public async Task<Result<RepairDTOEmployee>> UpdateRepair(int oldRepairId, RepairDTOEmployee newRepairDto)
    {
        
        var repairToUpdate = await _repairRepository.GetRepairById(oldRepairId);
        if (repairToUpdate == null)
        {
            return Result.Failure<RepairDTOEmployee>("The repair you want to update was not found");
        }

        Repair newRepair = Converters.ConvertToRepairEmployee(newRepairDto);

        if (repairToUpdate.Owner != null)
        {
            newRepair.Owner = repairToUpdate.Owner;
        }
        
        repairToUpdate = CloneEmp(repairToUpdate, newRepair);

        var repairUpdated = await _repairRepository.UpdateRepair(repairToUpdate);

        if (!repairUpdated)
        {
            return Result.Failure<RepairDTOEmployee>("Update failed");
        }
        return Result.Success(newRepairDto);
    }

    public async Task<Result> DeleteRepair(int repairId)
    {
        var repairToDelete = await _repairRepository.GetRepairById(repairId);
        if (repairToDelete == null)
        {
            return Result.Failure("This repair does not exist");
        }

        var repairDeleted = await _repairRepository.DeleteRepair(repairToDelete);

        return repairDeleted ? Result.Success("Repair successfully deleted") : Result.Failure("Delete failed");
    }

    public async Task<Result<RepairDTOOwner>> CreateRepairOwn(RepairDTOOwner repairDto, int ownerId) // the owner can only create certain fields unlike admin (excluding cost, repair status)
    {
        var owner = await _ownerRepository.GetOwnerById(ownerId);

        if (owner == null)
        {
            return Result.Failure<RepairDTOOwner>("Owner does not exist!");
        }

        Repair newRepair = Converters.ConvertToRepairOwner(repairDto);
        newRepair.Owner = owner;
        var repairCreated = await _repairRepository.CreateRepair(newRepair);
        if (!repairCreated)
        {
            return Result.Failure<RepairDTOOwner>("Failed to create repair!");
        }

        RepairDTOOwner createdRepairDto = Converters.ConvertToRepairDTOOwner(newRepair);

        return Result.Success(createdRepairDto);
    }

    public async Task<Result<RepairDTOOwner>> UpdateRepairOwn(int oldRepairId, RepairDTOOwner newRepairDto) // the owner can only update certain fields unlike admin (excluding cost, repair status)
    {
        var repairToUpdate = await _repairRepository.GetRepairById(oldRepairId);
        if (repairToUpdate == null)
        {
            return Result.Failure<RepairDTOOwner>("The repair you want to update was not found");
        }

        Repair newRepair = Converters.ConvertToRepairOwner(newRepairDto);

        if (repairToUpdate.Owner != null)
        {
            newRepair.Owner = repairToUpdate.Owner;
        }

        repairToUpdate = CloneOwn(repairToUpdate, newRepair);

        var repairUpdated = await _repairRepository.UpdateRepair(repairToUpdate);

        if (!repairUpdated)
        {
            return Result.Failure<RepairDTOOwner>("Update failed");
        }
        return Result.Success(newRepairDto);
    }

    private static Repair CloneEmp(Repair oldRepair, Repair newRepair)
    {
        oldRepair.Cost = newRepair.Cost;
        oldRepair.Description = newRepair.Description;
        oldRepair.Address = newRepair.Address;
        oldRepair.RepairStatus = newRepair.RepairStatus;
        oldRepair.RepairType = newRepair.RepairType;
        oldRepair.ScheduledRepair = newRepair.ScheduledRepair;

        return oldRepair;
    }

    private static Repair CloneOwn(Repair oldRepair, Repair newRepair)
    {
        oldRepair.Description = newRepair.Description;
        oldRepair.Address = newRepair.Address;
        oldRepair.RepairType = newRepair.RepairType;
        oldRepair.ScheduledRepair = newRepair.ScheduledRepair;

        return oldRepair;
    }
}