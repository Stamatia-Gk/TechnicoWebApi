
using TechnicoWebApi.Dtos;

namespace Technico.ViewModels;

public class CreatePropertyDto
{
    public List<OwnerResponseDto> ownerList = new ();
    public PropertyDto propertyDto;
}