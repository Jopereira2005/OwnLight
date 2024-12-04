using DeviceService.Application.Features.Device.Commands;
using FluentValidation;

namespace DeviceService.Application.Common.Validations.Device;

public class UpdateDeviceRoomValidator : AbstractValidator<UpdateDeviceRoomCommand>
{
    public UpdateDeviceRoomValidator()
    {
        RuleFor(x => x.RoomId).NotEmpty().WithMessage("RoomId is required.");
    }
}

public class UpdateDeviceGroupValidator : AbstractValidator<UpdateDeviceGroupCommand>
{
    public UpdateDeviceGroupValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId is required.");
    }
}
