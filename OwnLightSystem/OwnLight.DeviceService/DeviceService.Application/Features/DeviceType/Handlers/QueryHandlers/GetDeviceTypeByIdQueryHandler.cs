using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.DeviceType.Queries;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.DeviceType.Handlers.QueryHandlers;

public class GetDeviceTypeByIdQueryHandler(
    IDeviceTypeRepository deviceTypeRepository,
    IMapper mapper
) : IRequestHandler<GetDeviceTypeByIdQuery, DeviceTypeResponseDTO>
{
    private readonly IDeviceTypeRepository _deviceTypeRepository = deviceTypeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<DeviceTypeResponseDTO> Handle(
        GetDeviceTypeByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var deviceType =
            await _deviceTypeRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Device type with ID {request.Id} not found.");
        var response = _mapper.Map<DeviceTypeResponseDTO>(deviceType);
        return response;
    }
}
