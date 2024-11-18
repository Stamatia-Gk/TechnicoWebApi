// Team Project | European Dynamics | Code.Hub Project 2024
using CSharpFunctionalExtensions;
using Technico.Models;
using Technico.Services.Interfaces;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Repositories.Interfaces;

namespace TechnicoWebApi.Services.Implementations;

public class RepairService : IRepairService
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IRepairRepository _repairRepository;

    public RepairService(IOwnerRepository ownerRepository, IRepairRepository repairRepository)
    {
        _ownerRepository = ownerRepository;
        _repairRepository = repairRepository;
    }

    public async Task<Result<List<RepairDto>>> GetAllRepairs()
    {
        var repairs = await _repairRepository.GetRepairs();
        if (repairs.Count == 0)
        {
            return Result.Failure<List<RepairDto>>("No repairs found!");
        }
       
        var repairDtOs = repairs.Select(Converters.ConvertToRepairDto).ToList();
        return Result.Success(repairDtOs);
    }

    public async Task<Result<List<RepairDto>>> GetOngoingRepairs()
    {
        var onGoingRepairs = await _repairRepository.GetRepairsInProgress();
        if (onGoingRepairs.Count == 0)
        {
            return Result.Failure<List<RepairDto>>("No ongoing repairs found for this user");
        }

        var onGoingRepairsDtoList = onGoingRepairs.Select(r => Converters.ConvertToRepairDto(r)).ToList();
        return Result.Success(onGoingRepairsDtoList);
    }

    public async Task<Result<RepairDto>> GetRepairById(int id)
    {
        var repair = await _repairRepository.GetRepairById(id);
        if (repair == null)
        {
            return Result.Failure<RepairDto>("Repair not found!");
        }

        return Result.Success(Converters.ConvertToRepairDto(repair));
    }

    public async Task<Result<List<RepairDto>>> GetAllRepairsOfAnOwner(int id)
    {
        var ownerRepairs = await _repairRepository.GetRepairsByOwnerId(id);
        if (ownerRepairs.Count == 0)
        {
            return Result.Failure<List<RepairDto>>("No repairs found for this onwer.");
        }

        var onwerRepairsDTO = ownerRepairs.Select(r => Converters.ConvertToRepairDto(r)).ToList();
        return onwerRepairsDTO;
    }

    public async Task<Result<RepairDto>> CreateRepair(RepairDto repairDto, int ownerId)
    {
        var owner = await _ownerRepository.GetOwnerById(ownerId);
        if (owner == null)
        {
            return Result.Failure<RepairDto>("Owner does not exist!");
        }

        Repair newRepair = Converters.ConvertToRepairEmployee(repairDto);
        newRepair.Owner = owner;
        var repairCreated = await _repairRepository.CreateRepair(newRepair);
        if (!repairCreated)
        {
            return Result.Failure<RepairDto>("Failed to create repair.");
        }

        RepairDto createdRepairDto = Converters.ConvertToRepairDto(newRepair);
        return Result.Success(createdRepairDto);
    }

    public async Task<Result<RepairDto>> UpdateRepair(int oldRepairId, RepairDto newRepairDto)
    {
        var repairToUpdate = await _repairRepository.GetRepairById(oldRepairId);
        if (repairToUpdate == null)
        {
            return Result.Failure<RepairDto>("The repair you want to update was not found.");
        }

        var newRepair = Converters.ConvertToRepairEmployee(newRepairDto);
        if (repairToUpdate.Owner != null)
        {
            newRepair.Owner = repairToUpdate.Owner;
        }

        repairToUpdate = Clone(repairToUpdate, newRepair);
        var repairUpdated = await _repairRepository.UpdateRepair(repairToUpdate);

        if (!repairUpdated)
        {
            return Result.Failure<RepairDto>("Update failed.");
        }

        return Result.Success(newRepairDto);
    }

    public async Task<Result> DeleteRepair(int repairId)
    {
        var repairToDelete = await _repairRepository.GetRepairById(repairId);
        if (repairToDelete == null)
        {
            return Result.Failure("This repair does not exist!");
        }

        var repairDeleted = await _repairRepository.DeleteRepair(repairToDelete);
        return repairDeleted ? Result.Success("Repair successfully deleted.") : Result.Failure("Delete failed.");
    }

    public async Task<Result<List<RepairDto>>> SearchRepair(DateTime startDate, DateTime endDate, int userId)
    {
        if (endDate < startDate)
        {
            return Result.Failure<List<RepairDto>>("End date must be greater or equal than the start date.");
        }

        var owner = await _ownerRepository.GetOwnerById(userId);
        var repairs = owner != null ? await _repairRepository.Search(startDate, endDate, userId) : await _repairRepository.Search(startDate, endDate);
        if (repairs.Count == 0)
        {
            return Result.Failure<List<RepairDto>>("No repairs found with the specified criteria.");
        }

        var repairsDTO = repairs.Select(Converters.ConvertToRepairDto).ToList();
        return Result.Success(repairsDTO);
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
