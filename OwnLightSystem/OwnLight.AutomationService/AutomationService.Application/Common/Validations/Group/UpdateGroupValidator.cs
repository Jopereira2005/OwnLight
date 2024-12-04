using AutomationService.Application.Features.Group.Commands;
using FluentValidation;

namespace AutomationService.Application.Common.Validations.Group;

public class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(30)
            .WithMessage("Name must not exceed 30 characters")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters long");

        RuleFor(x => x.Description)
            .MaximumLength(100)
            .WithMessage("Description must not exceed 100 characters");
    }
}
