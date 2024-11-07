// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.DTO;
public class OwnerDTO
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
