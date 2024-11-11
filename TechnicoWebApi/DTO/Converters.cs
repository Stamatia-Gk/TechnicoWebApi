// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;

namespace Technico.DTO;

public static class Converters
{
    public static OwnerDTOCreate ConvertToOwnerPwDTO(this Owner owner) // Owner PW -> PW DTO
    {
        return new OwnerDTOCreate()
        {
            Id = owner.Id,
            VAT = owner.VAT,
            Name = owner.Name,
            Surname = owner.Surname,
            Address = owner.Address,
            PhoneNumber = owner.PhoneNumber,
            Email = owner.Email,
            OwnerType = owner.OwnerType,
            Password = owner.Password
        };
    }

    public static OwnerDTO ConvertToOwnerDTO(this Owner owner) // Owner -> DTO
    {
        return new OwnerDTO()
        {
            Id = owner.Id,
            VAT = owner.VAT,
            Name = owner.Name,
            Surname = owner.Surname,
            Address = owner.Address,
            PhoneNumber = owner.PhoneNumber,
            Email = owner.Email,
            OwnerType = owner.OwnerType,
        };
    }

    public static Owner ConvertToOwnerPw(this OwnerDTOCreate ownerDtoPw) // Pw DTO -> Owner PW
    {
        return new Owner()
        {
            Id = ownerDtoPw.Id,
            VAT = ownerDtoPw.VAT,
            Name = ownerDtoPw.Name,
            Surname = ownerDtoPw.Surname,
            Address = ownerDtoPw.Address,
            PhoneNumber = ownerDtoPw.PhoneNumber,
            Email = ownerDtoPw.Email,
            OwnerType = ownerDtoPw.OwnerType,
            Password = ownerDtoPw.Password
        };
    }

    public static Owner ConvertToOwner(this OwnerDTO ownerDto) // DTO -> Owner
    {
        return new Owner()
        {
            Id = ownerDto.Id,
            VAT = ownerDto.VAT,
            Name = ownerDto.Name,
            Surname = ownerDto.Surname,
            Address = ownerDto.Address,
            PhoneNumber = ownerDto.PhoneNumber,
            Email = ownerDto.Email,
            OwnerType = ownerDto.OwnerType
        };
    }

    public static OwnerWithPropertiesDTO ConvertToOwnerWithPropertiesDTO(this Owner owner)
    {
        return new OwnerWithPropertiesDTO()
        {
            Id = owner.Id,
            VAT = owner.VAT,
            Name = owner.Name,
            Surname = owner.Surname,
            Address = owner.Address,
            PhoneNumber = owner.PhoneNumber,
            Email = owner.Email,
            OwnerType = owner.OwnerType,
            OwnerProperties = owner.Properties.Select(p => p.ConvertToPropertyDTO()).ToList()
        };
    }

    public static OwnerWithRepairsDTO ConvertToOwnerWithRepairsDTO(this Owner owner)
    {
        return new OwnerWithRepairsDTO()
        {
            Id = owner.Id,
            VAT = owner.VAT,
            Name = owner.Name,
            Surname = owner.Surname,
            Address = owner.Address,
            PhoneNumber = owner.PhoneNumber,
            Email = owner.Email,
            OwnerType = owner.OwnerType,
            OwnerRepairs = owner.AllRepairs.Select(r => r.ConvertToRepairDTOOwner()).ToList()
        };
    }

    public static PropertyItem ConvertToPropertyItem(this PropertyDTO propertyDto)
    {
        return new PropertyItem()
        { 
            IdentificationNumber = propertyDto.IdentificationNumber,
            Address = propertyDto.Address,
            ConstructionYear = propertyDto.ConstructionYear,
            PropertyType = propertyDto.PropertyType
        };
    }

    public static PropertyDTO ConvertToPropertyDTO(this PropertyItem propertyItem)
    {
        return new PropertyDTO()
        {   Id = propertyItem.Id,
            IdentificationNumber = propertyItem.IdentificationNumber,
            Address = propertyItem.Address,
            ConstructionYear = propertyItem.ConstructionYear,
            PropertyType = propertyItem.PropertyType,
        };
    }

    public static RepairDTOEmployee ConvertToRepairDTOEmployee(this Repair repair) 
    {
        return new RepairDTOEmployee()
        {
            Id = repair.Id,
            Description = repair.Description,
            Address = repair.Address,
            Cost = repair.Cost,
            RepairStatus = repair.RepairStatus,
            RepairType = repair.RepairType,
            ScheduledRepair = repair.ScheduledRepair
        };
    }

    public static Repair ConvertToRepairEmployee(this RepairDTOEmployee repairDTO) 
    {
        return new Repair()
        {
            Description = repairDTO.Description,
            Address = repairDTO.Address,
            Cost = repairDTO.Cost,
            RepairStatus = repairDTO.RepairStatus,
            RepairType = repairDTO.RepairType,
            ScheduledRepair = repairDTO.ScheduledRepair,
        };
    }

    public static RepairDTOOwner ConvertToRepairDTOOwner(this Repair repair)
    {
        return new RepairDTOOwner()
        {
            Id = repair.Id,
            Description = repair.Description,
            Address = repair.Address,
            RepairType = repair.RepairType,
            ScheduledRepair = repair.ScheduledRepair
        };
    }

    public static Repair ConvertToRepairOwner(this RepairDTOOwner repairDTO)
    {
        return new Repair()
        {
            Description = repairDTO.Description,
            Address = repairDTO.Address,
            RepairType = repairDTO.RepairType,
            ScheduledRepair = repairDTO.ScheduledRepair,
        };
    }
}
