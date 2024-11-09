﻿// Team Project | European Dynamics | Code.Hub Project 2024
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
}
