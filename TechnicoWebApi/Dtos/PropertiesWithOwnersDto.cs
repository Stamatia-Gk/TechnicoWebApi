﻿// Team Project | European Dynamics | Code.Hub Project 2024

namespace TechnicoWebApi.Dtos;

public class PropertiesWithOwnersDto: PropertyDto
{
    public List<PropertyDto>? PropertyOwners { get; set; }
}