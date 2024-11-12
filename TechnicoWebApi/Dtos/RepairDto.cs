// Team Project | European Dynamics | Code.Hub Project 2024

using Technico.Models;
using TechnicoWebApi.Models;

namespace TechnicoWebApi.Dtos;

 public class RepairDto()
{
    public int Id { get; set; }
    public DateTime ScheduledRepair { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; } // repair's description
    public RepairType RepairType { get; set; }
    public RepairStatus RepairStatus { get; set; }
    public decimal Cost { get; set; }
}
