// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.DTO;

/*public class RepairDTO()
{
    public int Id { get; set; }
    public DateTime ScheduledRepair { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; } // repair's description
    public RepairType RepairType { get; set; }
    public RepairStatus RepairStatus { get; set; }
    public decimal Cost { get; set; }
    public OwnerDTO OwnerDto { get; set; } = new();
}*/

public record RepairDTO(int Id, DateTime ScheduledRepair, RepairType RepairType, string Description, string Address, RepairStatus RepairStatus, decimal Cost);
