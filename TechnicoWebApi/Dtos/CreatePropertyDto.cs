namespace TechnicoWebApi.Dtos;

public class CreatePropertyDto
{
    public List<OwnerResponseDto> ownerList = new ();
    public PropertyDto propertyDto;
}