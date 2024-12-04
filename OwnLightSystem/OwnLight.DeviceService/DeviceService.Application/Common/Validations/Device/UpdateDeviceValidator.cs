using DeviceService.Application.Features.Device.Commands;
using FluentValidation;

namespace DeviceService.Application.Common.Validations.Device;

public class UpdateDeviceValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.");
    }
}
