// Team Project | European Dynamics | Code.Hub Project 2024
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.DTO;

public class OwnerDTOCreate : OwnerDTO
{
    public string Password { get; set; } = string.Empty;
}
