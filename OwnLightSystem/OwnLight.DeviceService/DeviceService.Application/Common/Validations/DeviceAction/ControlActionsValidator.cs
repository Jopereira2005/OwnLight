using DeviceService.Application.Features.DeviceAction.Commands;
using FluentValidation;

namespace DeviceService.Application.Common.Validations.DeviceAction;

public class ControlAllUserDevicesValidator : AbstractValidator<ControlAllUserDevicesCommand>
{
    public ControlAllUserDevicesValidator()
    {
        RuleFor(x => x.Status).IsInEnum().WithMessage("Status should be 1 or 0.");
    }
}

public class ControlDeviceValidator : AbstractValidator<ControlDeviceCommand>
{
    public ControlDeviceValidator()
    {
        RuleFor(x => x.Status).IsInEnum().WithMessage("Status should be 1 or 0.");
    }
}

public class ControlGroupValidator : AbstractValidator<ControlGroupCommand>
{
    public ControlGroupValidator()
    {
        RuleFor(x => x.Status).IsInEnum().WithMessage("Status should be 1 or 0.");
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId is required.");
    }
}

public class ControlRoomValidator : AbstractValidator<ControlRoomCommand>
{
    public ControlRoomValidator()
    {
        RuleFor(x => x.Status).IsInEnum().WithMessage("Status should be 1 or 0.");
        RuleFor(x => x.RoomId).NotEmpty().WithMessage("RoomId is required.");
    }
}