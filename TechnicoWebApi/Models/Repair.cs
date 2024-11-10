// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Technico.Models;

public class Repair
{
    public int Id { get; set; }
    public DateTime ScheduledRepair { get; set; }
    public RepairType RepairType { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; } // repair's description

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    public RepairStatus RepairStatus { get; set; }

    [Precision(8, 2)]
    public decimal Cost { get; set; }
    public Owner Owner { get; set; } = new();
}
