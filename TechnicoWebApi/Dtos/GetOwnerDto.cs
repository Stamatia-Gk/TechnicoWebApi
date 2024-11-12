// Team Project | European Dynamics | Code.Hub Project 2024

using Technico.Models;
using TechnicoWebApi.Models;

namespace TechnicoWebApi.Dtos;
public class GetOwnerDto
{
    public int Id { get; set; }
    public string VAT { get; set; } = string.Empty; // unique identifier
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; // used as username
    public OwnerType OwnerType { get; set; }
}
