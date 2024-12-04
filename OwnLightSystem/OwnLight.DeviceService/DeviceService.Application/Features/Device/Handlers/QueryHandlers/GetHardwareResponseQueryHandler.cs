using AutoMapper;
using DeviceService.Application.DTOs;
using DeviceService.Application.Features.Device.Queries;
using DeviceService.Domain.Interfaces;
using MediatR;

namespace DeviceService.Application.Features.Device.Handlers.QueryHandlers;

public class GetHardwareResponseQueryHandler(IMapper mapper, IDeviceRepository deviceRepository)
    : IRequestHandler<GetHardwareResponseQuery, IEnumerable<HardwareResponseDTO>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IDeviceRepository _deviceRepository = deviceRepository;

    public async Task<IEnumerable<HardwareResponseDTO>> Handle(
        GetHardwareResponseQuery request,
        CancellationToken cancellationToken
    )
    {
        if (request.DeviceIds == null || request.DeviceIds.Length == 0)
            throw new ArgumentException("É necessário informar ao menos um id de dispositivo.");

        var devices = await _deviceRepository.GetDevicesByIdsAsync(request.DeviceIds);

        if (!devices.Any())
            throw new KeyNotFoundException("Nenhum dispositivo encontrado para os IDs fornecidos.");

        var devicesDTO = _mapper.Map<IEnumerable<HardwareResponseDTO>>(devices);

        return devicesDTO;
    }
}
