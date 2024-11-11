// Team Project | European Dynamics | Code.Hub Project 2024
using FluentValidation;
using Technico.DTO;

namespace Technico.Validator;

public class RepairValidator : AbstractValidator<RepairDTO>
{
    public RepairValidator()
    {
        RuleFor(r => r.RepairType)
            .IsInEnum()
            .WithMessage("Invalid repair type specified.");

        RuleFor(r => r.ScheduledRepair)
            .NotEmpty()
            .WithMessage("Repair date and time must be specified.")
            .GreaterThanOrEqualTo(DateTime.Now);

        RuleFor(r => r.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");

        RuleFor(r => r.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MaximumLength(200)
            .WithMessage("Address cannot exceed 200 characters.");

        RuleFor(r => r.RepairStatus)
            .IsInEnum()
            .WithMessage("Invalid status specified.");

        RuleFor(r => r.Cost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Cost cannot be negative.")
            .PrecisionScale(8, 2, true)
            .WithMessage("Cost must have up to 10 digits in total with 2 decimal places.");
    }
}
