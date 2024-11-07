// Team Project | European Dynamics | Code.Hub Project 2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.DTO;

public class OwnerWithRepairsDTO: OwnerDTO
{
    public List<OwnerDTO>? OwnerRepairs { get; set; }
}
