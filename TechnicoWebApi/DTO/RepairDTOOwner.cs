// Team Project | European Dynamics | Code.Hub Project 2024

using Technico.Models;

namespace Technico.DTO;

public class RepairDTOOwner()
{
    public int Id { get; set; }
    public DateTime ScheduledRepair { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public RepairType RepairType { get; set; }
}
