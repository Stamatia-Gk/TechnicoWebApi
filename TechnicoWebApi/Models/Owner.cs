// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class Owner
{
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public string VAT { get; set; } = string.Empty; // unique identifier
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Surname { get; set; } = string.Empty;
    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(250)]
    public string Email { get; set; } = string.Empty; // used as username
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    public OwnerType OwnerType { get; set; } // this may not be needed since we only develop the home owner side and not the professional repairer (both users but we dev the owner)

    public List<PropertyItem> Properties { get; set; } = []; // an owner can have one or more properties
    public List<Repair> AllRepairs { get; set; } = []; // a property can have multiple repairs
}
