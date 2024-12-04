using DeviceService.Application.Features.Device.Commands;
using FluentValidation;

namespace DeviceService.Application.Common.Validations.Device;

public class CreateDeviceValidator : AbstractValidator<CreateDeviceCommand>
{
    public CreateDeviceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.IsDimmable).NotNull().WithMessage("IsDimmable is required.");
    }
}
