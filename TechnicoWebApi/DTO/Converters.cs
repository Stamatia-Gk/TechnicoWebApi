// Team Project | European Dynamics | Code.Hub Project 2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            OwnerType = ownerDto.OwnerType
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
            OwnerDto = new OwnerDTO()
            {
                Id = repair.Owner.Id,
                Address = repair.Owner.Address,
                Email = repair.Owner.Email,
                Name = repair.Owner.Name,
                OwnerType = repair.Owner.OwnerType,
                PhoneNumber = repair.Owner.PhoneNumber,
                Surname = repair.Owner.Surname,
                VAT = repair.Owner.VAT,
            }
        };
    }

    public static Repair ConvertToRepair (this RepairDTO repairDTO) 
    {
        return new Repair()
        {
            Id = repairDTO.Id,
            Description = repairDTO.Description,
            Address = repairDTO.Address,
            Cost = repairDTO.Cost,
            RepairStatus = repairDTO.RepairStatus,
            RepairType = repairDTO.RepairType,
            ScheduledRepair = repairDTO.ScheduledRepair,
            Owner = new Owner()
            {
               Id = repairDTO.OwnerDto.Id,
               Address = repairDTO.OwnerDto.Address,
               Email= repairDTO.OwnerDto.Email,
               Name= repairDTO.OwnerDto.Name,
               Surname = repairDTO.OwnerDto.Surname,
               VAT = repairDTO.OwnerDto.VAT,
               OwnerType = repairDTO.OwnerDto.OwnerType,
               PhoneNumber = repairDTO.OwnerDto.PhoneNumber,
            }
        };
    }
}
