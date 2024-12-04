using AutoMapper;
using DeviceService.Application.Features.Device.Commands;
using DeviceService.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace DeviceService.Application.Features.Device.Handlers.CommandHandlers;

public class UpdateDeviceGroupCommandHandler(
    IDeviceRepository deviceRepository,
    IMapper mapper,
    IValidator<UpdateDeviceGroupCommand> validator
) : IRequestHandler<UpdateDeviceGroupCommand>
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateDeviceGroupCommand> _validator = validator;

    public async Task<Unit> Handle(
        UpdateDeviceGroupCommand request,
        CancellationToken cancellationToken
    )
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        var device =
            await _deviceRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Device with ID {request.Id} not found.");

        _mapper.Map(request, device);

        await _deviceRepository.UpdateAsync(device, cancellationToken);

        return Unit.Value;
    }
}
