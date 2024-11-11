// Team Project | European Dynamics | Code.Hub Project 2024
using FluentValidation;
using Technico.DTO;

namespace Technico.Validator;

public class OwnerValidator : AbstractValidator<OwnerDTOCreate>
{
    public OwnerValidator()
    {
        RuleFor(o => o.Email).EmailAddress().MaximumLength(50);
        RuleFor(owner => owner.PhoneNumber)
            .NotEmpty().MaximumLength(10).WithMessage("Phone number is required.")
            .Matches(@"^69\d{8}$").WithMessage("Invalid phone number format. Expected format: 6911111111.");

        RuleFor(o => o.Name).NotEmpty().MaximumLength(20).WithMessage("Name cannot be empty.");
        RuleFor(o => o.Surname).NotEmpty().MaximumLength(20).WithMessage("Surname cannot be empty.");

        RuleFor(o => o.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$").WithMessage("Password must contain a minimum of eight characters, " +
                                                                            "at least one uppercase letter, one lowercase letter, one number and one special character.");
    }
}
