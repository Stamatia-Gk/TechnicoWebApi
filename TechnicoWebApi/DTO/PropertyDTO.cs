// Team Project | European Dynamics | Code.Hub Project 2024
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.DTO;

public class PropertyDTO()
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty; // unique
    public string Address { get; set; } = string.Empty;
    public int ConstructionYear { get; set; }
    public PropertyType PropertyType { get; set; }
}


