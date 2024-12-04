using DeviceService.Application.Features.DeviceType.Commands;
using FluentValidation;

namespace DeviceService.Application.Common.Validations.DeviceType;

public class UpdateDeviceTypeValidator : AbstractValidator<UpdateDeviceTypeCommand>
{
    public UpdateDeviceTypeValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters.");
    }
}
