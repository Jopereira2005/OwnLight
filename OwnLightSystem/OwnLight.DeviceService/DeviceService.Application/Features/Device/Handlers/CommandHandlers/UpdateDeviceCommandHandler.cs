using AutoMapper;
using DeviceService.Application.Features.Device.Commands;
using DeviceService.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeviceService.Application.Features.Device.Handlers.CommandHandlers;

public class UpdateDeviceCommandHandler(
    IDeviceRepository deviceRepository,
    IMapper mapper,
    IValidator<UpdateDeviceCommand> validator,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<UpdateDeviceCommand>
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<UpdateDeviceCommand> _validator = validator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Unit> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        var device =
            await _deviceRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Device with ID {request.Id} not found.");

        var deviceExists = await _deviceRepository.GetDevicesByUserIdAsync(
            Guid.Parse(userId),
            pageNumber: 1,
            pageSize: 10,
            cancellationToken
        );
        if (deviceExists.Any(d => d.Name == request.Name && d.Id != request.Id))
            throw new ArgumentException($"Dispositivo com o nome '{request.Name}' já existe.");

        _mapper.Map(request, device);
        await _deviceRepository.UpdateAsync(device, cancellationToken);

        return Unit.Value;
    }
}
