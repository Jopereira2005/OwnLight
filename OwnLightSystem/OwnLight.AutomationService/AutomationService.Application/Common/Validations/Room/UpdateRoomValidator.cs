using AutomationService.Application.Features.Room.Commands;
using FluentValidation;

namespace AutomationService.Application.Common.Validations.Room;

public class UpdateRoomValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.")
            .MinimumLength(3)
            .WithMessage("Name must be at least 3 characters.");
    }
}
