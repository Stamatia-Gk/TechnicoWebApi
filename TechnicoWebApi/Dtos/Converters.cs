// Team Project | European Dynamics | Code.Hub Project 2024

using Technico.Models;
using TechnicoWebApi.Models;

namespace TechnicoWebApi.Dtos;

public static class Converters
{
    public static CreateOwnerDto ConvertToOwnerPwDto(this Owner owner) // Owner PW -> PW DTO
    {
        return new CreateOwnerDto()
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

    public static GetOwnerDto ConvertToOwnerDto(this Owner owner) // Owner -> DTO
    {
        return new GetOwnerDto()
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

    public static Owner ConvertToOwnerPw(this CreateOwnerDto createOwnerDtoPw) // Pw DTO -> Owner PW
    {
        return new Owner()
        {
            Id = createOwnerDtoPw.Id,
            VAT = createOwnerDtoPw.VAT,
            Name = createOwnerDtoPw.Name,
            Surname = createOwnerDtoPw.Surname,
            Address = createOwnerDtoPw.Address,
            PhoneNumber = createOwnerDtoPw.PhoneNumber,
            Email = createOwnerDtoPw.Email,
            OwnerType = createOwnerDtoPw.OwnerType,
            Password = createOwnerDtoPw.Password
        };
    }

    public static Owner ConvertToOwner(this GetOwnerDto getOwnerDto) // DTO -> Owner
    {
        return new Owner()
        {
            Id = getOwnerDto.Id,
            VAT = getOwnerDto.VAT,
            Name = getOwnerDto.Name,
            Surname = getOwnerDto.Surname,
            Address = getOwnerDto.Address,
            PhoneNumber = getOwnerDto.PhoneNumber,
            Email = getOwnerDto.Email,
            OwnerType = getOwnerDto.OwnerType
        };
    }

    public static PropertyItem ConvertToPropertyItem(this PropertyDto propertyDto)
    {
        return new PropertyItem()
        { 
            IdentificationNumber = propertyDto.IdentificationNumber,
            Address = propertyDto.Address,
            ConstructionYear = propertyDto.ConstructionYear,
            PropertyType = propertyDto.PropertyType
        };
    }

    public static PropertyDto ConvertToPropertyDto(this PropertyItem propertyItem)
    {
        return new PropertyDto()
        {   Id = propertyItem.Id,
            IdentificationNumber = propertyItem.IdentificationNumber,
            Address = propertyItem.Address,
            ConstructionYear = propertyItem.ConstructionYear,
            PropertyType = propertyItem.PropertyType,
        };
    }

    public static RepairDto ConvertToRepairDto(this Repair repair) 
    {
        return new RepairDto()
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

    public static Repair ConvertToRepairEmployee(this RepairDto repairDto) 
    {
        return new Repair()
        {
            Description = repairDto.Description,
            Address = repairDto.Address,
            Cost = repairDto.Cost,
            RepairStatus = repairDto.RepairStatus,
            RepairType = repairDto.RepairType,
            ScheduledRepair = repairDto.ScheduledRepair,
        };
    }
}
