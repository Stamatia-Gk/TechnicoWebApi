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
    private readonly IRepairRepository _repairRepository;
    private readonly RepairValidator _repairValidator;

    public RepairService(IRepairRepository repairRepository, RepairValidator repairValidator)
    {
        _repairRepository = repairRepository;
        _repairValidator = repairValidator;
    }

    public async Task<Result<RepairDTO>> CreateRepair(Repair repair, Owner owner)
    {
        if (!(await _repairValidator.ValidateAsync(repair)).IsValid)
        {
            return Result.Failure<RepairDTO>("Invalid input");
        }

        var repairCreated = await _repairRepository.CreateRepair(repair, owner);
        if (!repairCreated)
        {
            return Result.Failure<RepairDTO>("Owner does not exist");
        }

        var repairDTO = MapToRepairDTO(repair);

        return Result.Success(repairDTO);
    }

    public async Task<Result<RepairDTO>> GetRepair(int id)
    {
        var repair = await _repairRepository.GetRepair(id);
        if (repair == null)
        {
            return Result.Failure<RepairDTO>("Repair not found");
        }

        var repairDTO = MapToRepairDTO(repair);

        return Result.Success(repairDTO);
    }

    public async Task<Result<RepairDTO>> UpdateRepair(int oldRepairId, Repair newRepair)
    {
        var repairToUpdate = await _repairRepository.GetRepair(oldRepairId);
        if (repairToUpdate == null)
        {
            return Result.Failure<RepairDTO>("The repair you want to update was not found");
        }

        var oldOwner = repairToUpdate.Owner;
        repairToUpdate = newRepair;

        if (newRepair.Owner == null)
        {
            repairToUpdate.Owner = oldOwner;
        }

        var repairUpdated = await _repairRepository.UpdateRepair(repairToUpdate);

        if (!repairUpdated)
        {
            return Result.Failure<RepairDTO>("Update failed");
        }

        var repairDTO = MapToRepairDTO(repairToUpdate);
        return Result.Success(repairDTO);
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

    private static RepairDTO MapToRepairDTO(Repair repair) // DTO
    {
        var repairDTO = new RepairDTO(repair.Id, repair.ScheduledRepair, repair.RepairType, repair.Description,
            repair.Address, repair.RepairStatus, repair.Cost);

        return repairDTO;
    }
}
