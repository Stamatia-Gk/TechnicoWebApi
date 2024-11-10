// Team Project | European Dynamics | Code.Hub Project 2024
using Technico.Models;

namespace Technico.DTO;

public static class Converters
{
    public static OwnerDTOCreate ConvertToOwnerPwDTO(this Owner owner)
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

    public static OwnerDTO ConvertToOwnerDTO(this Owner owner)
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

    public static Owner ConvertToOwnerPw(this OwnerDTOCreate ownerDtoPw)
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

    public static Owner ConvertToOwner(this OwnerDTO ownerDto)
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

    public static PropertyItem ConvertPropertyItem(PropertyDTO propertyDto)
    {
        return new PropertyItem()
        { 
            IdentificationNumber = propertyDto.IdentificationNumber,
            Address = propertyDto.Address,
            ConstructionYear = propertyDto.ConstructionYear,
            PropertyType = propertyDto.PropertyType,

        };
    }

    public static PropertyDTO ConvertToPropertyDto(PropertyItem propertyItem)
    {
        return new PropertyDTO()
        {   Id = propertyItem.Id,
            IdentificationNumber = propertyItem.IdentificationNumber,
            Address = propertyItem.Address,
            ConstructionYear = propertyItem.ConstructionYear,
            PropertyType = propertyItem.PropertyType,
        };
    }

    /*public static PropertyDTO ConvertProperty(this PropertyItem owner)
    {
        return new PropertyDTO()
        {
            //Id = owner.Id,
        };
    }*/

    public static RepairDTO ConvertToRepairDTO(this Repair repair) 
    {
        return new RepairDTO()
        {
            Id = repair.Id,
            Description = repair.Description,
            Address = repair.Address,
            Cost = repair.Cost,
            RepairStatus = repair.RepairStatus,
            RepairType = repair.RepairType,
            ScheduledRepair = repair.ScheduledRepair,
           
        };
    }

    public static Repair ConvertToRepair (this RepairDTO repairDTO) 
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
}
