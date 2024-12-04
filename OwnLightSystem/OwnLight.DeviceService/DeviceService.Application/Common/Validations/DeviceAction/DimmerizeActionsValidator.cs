using DeviceService.Application.Features.DeviceAction.Commands;
using FluentValidation;

namespace DeviceService.Application.Common.Validations.DeviceAction;

public class DimmerizeDeviceValidator : AbstractValidator<DimmerizeDeviceCommand>
{
    public DimmerizeDeviceValidator()
    {
        RuleFor(x => x.Brightness)
            .InclusiveBetween(0, 100)
            .WithMessage("Brightness should be between 0 and 100.");
        RuleFor(x => x.DeviceId).NotEmpty().WithMessage("DeviceId is required.");
    }
}

public class DimmerizeGroupValidator : AbstractValidator<DimmerizeGroupCommand>
{
    public DimmerizeGroupValidator()
    {
        RuleFor(x => x.Brightness)
            .InclusiveBetween(0, 100)
            .WithMessage("Brightness should be between 0 and 100.");
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId is required.");
    }
}

public class DimmerizeRoomValidator : AbstractValidator<DimmerizeRoomCommand>
{
    public DimmerizeRoomValidator()
    {
        RuleFor(x => x.Brightness)
            .InclusiveBetween(0, 100)
            .WithMessage("Brightness should be between 0 and 100.");
        RuleFor(x => x.RoomId).NotEmpty().WithMessage("RoomId is required.");
    }
}
