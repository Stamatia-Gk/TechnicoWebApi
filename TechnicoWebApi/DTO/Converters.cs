﻿// Team Project | European Dynamics | Code.Hub Project 2024
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
}
