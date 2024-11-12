// Team Project | European Dynamics | Code.Hub Project 2024
using FluentValidation;
using TechnicoWebApi.Dtos;

namespace Technico.Validator;

public class PropertyValidator : AbstractValidator<PropertyDto>
{
    public PropertyValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MinimumLength(5).WithMessage("Address must be at least 5 characters long.")
            .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.");

        RuleFor(x => x.ConstructionYear)
            .InclusiveBetween(1800, DateTime.Now.Year)
            .WithMessage("Year of construction must be between 1800 and the current year.");

        RuleFor(p => p.PropertyType)
            .IsInEnum()
            .WithMessage("Property type must be a valid enum value.");
    }
}
